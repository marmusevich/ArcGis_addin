using System;
using System.Collections.Generic;
using System.Text;
using System.Runtime.InteropServices;
using System.ComponentModel;
using ESRI.ArcGIS.Geodatabase;


namespace DemoTest
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
                AddData();
                wkspcEdit = ((IDataset)wrappedTable).Workspace as IWorkspaceEdit;
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
        private void AddData()
        {
            ICursor cur = null;

            if (sortedFilds == null)
            {
                // без сортировки
                cur = wrappedTable.Search(null, false);
            }
            else
            {
                // с сортировкой
                ITableSort tableSort = new TableSort();
                tableSort.Table = wrappedTable;
                tableSort.Fields = sortedFilds;
                tableSort.Sort(null);
                cur = tableSort.Rows;
            }

            IRow curRow = cur.NextRow();
            while (null != curRow)
            {
                Add(curRow);
                curRow = cur.NextRow();
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

        // inner members
        private ITable wrappedTable;
        private List<PropertyDescriptor> fakePropertiesList = new List<PropertyDescriptor>();
        private IWorkspaceEdit wkspcEdit;
        private string sortedFilds;

        #endregion
    }

}
