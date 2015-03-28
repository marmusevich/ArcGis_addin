using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;


namespace WorckWithReestr
{
    // класс связи АркГИС таблици и элементов упровления .NET
    public class TableWraper : BindingList<IRow>, ITypedList
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  public
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //конструктор, параметры
        //   tableToWrap - таблица для связи
        //   fildsToSorted - поля для сортировки
        //   queryFiltered - фильтр
        public TableWraper(ITable tableToWrap, string defaultFildsToSorted = null, IQueryFilter queryFiltered = null)
        {
            wrappedTable = tableToWrap;
            queryFilter = queryFiltered;
            defaultSortedFilds = defaultFildsToSorted;
            GenerateFakeProperties();
            GetData(defaultFildsToSorted);
            wkspcEdit = ((IDataset)wrappedTable).Workspace as IWorkspaceEdit;
            UseCVDomains = true;
            AllowNew = true;
            AllowRemove = true;
        }

        public PropertyDescriptorCollection GetItemProperties(PropertyDescriptor[] listAccessors)
        {
            PropertyDescriptorCollection propCollection = null;
            if (null == listAccessors)
            {
                propCollection = new PropertyDescriptorCollection(fakePropertiesList.ToArray());
            }
            else
            {
                List<PropertyDescriptor> tempList = new List<PropertyDescriptor>();
                foreach (PropertyDescriptor curPropDesc in listAccessors)
                {
                    if (fakePropertiesList.Contains(curPropDesc))
                    {
                        tempList.Add(curPropDesc);
                    }
                }
                propCollection = new PropertyDescriptorCollection(tempList.ToArray());
            }

            return propCollection;
        }

        public string GetListName(PropertyDescriptor[] listAccessors)
        {
            return ((IDataset)wrappedTable).Name;
        }

        //получить описатель свойства по имени поля
        public PropertyDescriptor GetPropertyDescriptorByName(string fildName)
        {
            PropertyDescriptor ret = null;
            foreach (PropertyDescriptor curPropDesc in fakePropertiesList)
            {
                if (curPropDesc.Name.Equals(fildName))
                    ret = curPropDesc;
            }
            return ret;
        }

        //получить описатель свойства по индексу
        public PropertyDescriptor GetPropertyDescriptorByIndex(int index)
        {
            PropertyDescriptor ret = null;
            if (index >= 0 && index < fakePropertiesList.Count)
                ret = fakePropertiesList[index];
            return ret;
        }

        //получить именя поля по индексу
        public string GetNameByIndex(int index)
        {
            string ret = null;
            if (index >= 0 && index < fakePropertiesList.Count)
                ret = fakePropertiesList[index].Name;
            return ret;
        }

        //получить индекс по имени поля
        public int GetIndexByName(string fildName)
        {
            int ret = -1;
            //foreach (PropertyDescriptor curPropDesc in fakePropertiesList)
            for(int i = 0; i < fakePropertiesList.Count; i++)
            {
                if (fakePropertiesList[i].Name.Equals(fildName))
                    ret = i;
            }
            return ret;
        }

        //включить выключить поддержку доменов
        public bool UseCVDomains
        {
            set
            {
                foreach (FieldPropertyDescriptor curPropDesc in fakePropertiesList)
                {
                    if (curPropDesc.HasCVDomain)
                    {
                        curPropDesc.UseCVDomain = value;
                    }
                }
            }
        }
        #endregion
      
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  для редактирования
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            if (AllowNew)
            {
                IRow newRow = wrappedTable.CreateRow();
                e.NewObject = newRow;
                for (int fieldCount = 0; fieldCount < newRow.Fields.FieldCount; fieldCount++)
                {
                    IField curField = newRow.Fields.get_Field(fieldCount);
                    if (curField.Editable)
                    {
                        newRow.set_Value(fieldCount, (object)curField.DefaultValue);
                    }
                }
                bool weStartedEditing = StartEditOp();
                newRow.Store();
                StopEditOp(weStartedEditing);

                base.OnAddingNew(e);
            }
        }

        protected override void RemoveItem(int index)
        {
            if (AllowRemove)
            {
                IRow itemToRemove = Items[index];
                bool weStartedEditing = StartEditOp();
                itemToRemove.Delete();
                StopEditOp(weStartedEditing);
                base.RemoveItem(index);
            }
        }
        
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (!isGetingData)
                base.OnListChanged(e);
        }


        private bool StartEditOp()
        {
            bool retVal = false;
            if (!wkspcEdit.IsBeingEdited())
            {
                wkspcEdit.StartEditing(false);
                retVal = true;
            }
            wkspcEdit.StartEditOperation();
            return retVal;
        }

        private void StopEditOp(bool weStartedEditing)
        {
            wkspcEdit.StopEditOperation();
            if (weStartedEditing)
            {
                wkspcEdit.StopEditing(true);
            }
        }
        #endregion
        
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  получение данных
        //---------------------------------------------------------------------------------------------------------------------------------------------
        // создать перечень колонок в связке
        private void GenerateFakeProperties()
        {
            for (int fieldCount = 0; fieldCount < wrappedTable.Fields.FieldCount; fieldCount++)
            {
                FieldPropertyDescriptor newPropertyDesc = new FieldPropertyDescriptor(wrappedTable, wrappedTable.Fields.get_Field(fieldCount).Name, fieldCount);
                fakePropertiesList.Add(newPropertyDesc);
            }
        }
        // выгрузить данные
        private void GetData(string fildName = null, bool isSortDirectionAscending = true)
        {
            isGetingData = true; // экранировать изменения
            ClearItems();
            ICursor cur = null;
            if (fildName == null)
            {
                // без сортировки
                cur = wrappedTable.Search(queryFilter, false);
            }
            else
            {
                // собственно сортировка здесь
                ITableSort tableSort = new TableSort();
                tableSort.Table = wrappedTable;
                tableSort.Fields = fildName;
                tableSort.set_Ascending(fildName, isSortDirectionAscending);

                if (queryFilter != null)
                    tableSort.QueryFilter = queryFilter;
                if (selectionSet != null)
                    tableSort.SelectionSet = selectionSet;

                tableSort.Sort(null);
                cur = tableSort.Rows;
            }
            IRow curRow = cur.NextRow();
            while (null != curRow)
            {
                Add(curRow);
                curRow = cur.NextRow();
            }
            isGetingData = false;
        }

        public void UpdateData()
        {
            if (sortProperty == null)
            {
                RemoveSortCore();

                //GetData(defaultSortedFilds);
                //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
            }
            else
            {
                //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
                ApplySortCore(sortProperty, sortDirection);
            }

            OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1)); 
            //ListChanged(this, new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #endregion
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  для поиска, фильтрации
        //---------------------------------------------------------------------------------------------------------------------------------------------
        //protected override bool SupportsSearchingCore
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}
        //protected override int FindCore(System.ComponentModel.PropertyDescriptor prop, object key)
        //{
        //    //return base.FindCore(prop, key);
        //    return -1;
        //}
        #endregion
       
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  сортировка
        //---------------------------------------------------------------------------------------------------------------------------------------------

        protected override bool SupportsSortingCore
        {
            get
            {
                return true;
            }
        }
        protected override bool IsSortedCore
        {
            get
            {
                return isSorted;
            }
        }
        protected override ListSortDirection SortDirectionCore
        {
            get
            {
                return sortDirection;
            }
        }
        protected override void ApplySortCore(PropertyDescriptor prop, ListSortDirection direction)
        {
            isSorted = true;
            sortDirection = direction;
            sortProperty = prop;

            if (sortProperty == null)
                GetData(null, sortDirection == ListSortDirection.Ascending);
            else
                GetData(sortProperty.Name, sortDirection == ListSortDirection.Ascending);

            // по правилам нужно, но у меня лучьше без этого
            //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        protected override void RemoveSortCore()
        {
            isSorted = false;
            sortDirection = ListSortDirection.Ascending;
            sortProperty = null;

            // отмена сортировки
            GetData(defaultSortedFilds);
            // по правилам нужно, но у меня лучьше без этого
            //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        #endregion
       
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  переменные
        //---------------------------------------------------------------------------------------------------------------------------------------------
        protected ITable wrappedTable;
        protected List<PropertyDescriptor> fakePropertiesList = new List<PropertyDescriptor>();
        protected IWorkspaceEdit wkspcEdit;
        private bool isGetingData = false;

        // поле для сортировки по умолчанию
        protected string defaultSortedFilds;

        // для фмльтрации
        protected IQueryFilter queryFilter = null;
        protected ISelectionSet selectionSet = null;

        //для сортировки
        protected bool isSorted = false;
        protected ListSortDirection sortDirection = ListSortDirection.Ascending;
        protected PropertyDescriptor sortProperty = null;

        #endregion
    }

}
