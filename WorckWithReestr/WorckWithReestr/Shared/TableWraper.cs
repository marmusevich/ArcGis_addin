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
        public TableWraper(ITable tableToWrap, string fildsToSorted = null)
            {
                wrappedTable = tableToWrap;
                sortedFilds = fildsToSorted;
                GenerateFakeProperties();
                GetData(fildsToSorted);
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
                // Return all properties
                propCollection = new PropertyDescriptorCollection(fakePropertiesList.ToArray());
            }
            else
            {
                // Return the requested properties by checking each item in listAccessors
                // to make sure it exists in our property collection.
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

        public bool UseCVDomains
        {
            set
            {
                foreach (FieldPropertyDescriptor curPropDesc in fakePropertiesList)
                {
                    if (curPropDesc.HasCVDomain)
                    {
                        // Field has a coded value domain so turn the usage of this on or off
                        // as requested
                        curPropDesc.UseCVDomain = value;
                    }
                }
            }
        }
    #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  private 
        //---------------------------------------------------------------------------------------------------------------------------------------------

        
        // для редактирования
        protected override void OnAddingNew(AddingNewEventArgs e)
        {
            // Check that we can still add rows, this property could have been changed
            if (AllowNew)
            {
                // Need to create a new IRow
                IRow newRow = wrappedTable.CreateRow();
                e.NewObject = newRow;

                // Loop through fields and set default values
                for (int fieldCount = 0; fieldCount < newRow.Fields.FieldCount; fieldCount++)
                {
                    IField curField = newRow.Fields.get_Field(fieldCount);
                    if (curField.Editable)
                    {
                        newRow.set_Value(fieldCount, (object)curField.DefaultValue);
                    }
                }

                // Save default values
                bool weStartedEditing = StartEditOp();
                newRow.Store();
                StopEditOp(weStartedEditing);

                base.OnAddingNew(e);
            }
        }

        protected override void RemoveItem(int index)
        {
            // Check that we can still delete rows, this property could have been changed
            if (AllowRemove)
            {
                // Get the corresponding IRow
                IRow itemToRemove = Items[index];

                bool weStartedEditing = StartEditOp();

                // Delete the row
                itemToRemove.Delete();

                StopEditOp(weStartedEditing);

                base.RemoveItem(index);
            }
        }

        private bool StartEditOp()
        {
            bool retVal = false;

            // Check to see if we're editing
            if (!wkspcEdit.IsBeingEdited())
            {
                // Not being edited so start here
                wkspcEdit.StartEditing(false);
                retVal = true;
            }

            // Start operation
            wkspcEdit.StartEditOperation();
            return retVal;
        }

        private void StopEditOp(bool weStartedEditing)
        {
            // Stop edit operation
            wkspcEdit.StopEditOperation();

            if (weStartedEditing)
            {
                // We started the edit session so stop it here
                wkspcEdit.StopEditing(true);
            }
        }
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
            isGetingData = true;

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
        protected override void OnListChanged(ListChangedEventArgs e)
        {
            if (!isGetingData)
                base.OnListChanged(e);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------


        //// для поиска, фильтрации
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
        //---------------------------------------------------------------------------------------------------------------------------------------------

        
        //сортировка
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
            GetData(prop.Name, sortDirection == ListSortDirection.Ascending);
            //--
            isSorted = true;
            sortDirection = direction;
            sortProperty = prop;

            //OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }
        protected override void RemoveSortCore()
        {
            isSorted = false;
            sortDirection = ListSortDirection.Ascending;
            sortProperty = null;

            // отмена сортировки
            GetData(sortedFilds);
        }
        //---------------------------------------------------------------------------------------------------------------------------------------------

        #endregion


        // переменные
        protected ITable wrappedTable;
        protected List<PropertyDescriptor> fakePropertiesList = new List<PropertyDescriptor>();
        protected IWorkspaceEdit wkspcEdit;
        // поле для сортировки по умолчанию
        protected string sortedFilds;
        private bool isGetingData = false;

        // для фмльтрации
        protected IQueryFilter queryFilter = null;
        protected ISelectionSet selectionSet = null;

        //для сортировки
        protected bool isSorted = false;
        protected ListSortDirection sortDirection = ListSortDirection.Ascending;
        protected PropertyDescriptor sortProperty = null;




    }

}
