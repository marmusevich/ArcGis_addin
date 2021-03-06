﻿using System.Collections;
using ESRI.ArcGIS.Geodatabase;
using System;


namespace SharedClasses
{
    //обертка над перечисляемыми доменнами
    public class DomeinDataAdapter
    {
        //создать адаптер домена, установить лист значений комбобокса, и установить значение по умолчанию
        public static void CreateDomeinDataAdapterAndAddRangeToComboBoxAndSetDefaultValue(ref System.Windows.Forms.ComboBox cb, ref DomeinDataAdapter dda, ref ITable table, string fildName)
        {
            dda = new DomeinDataAdapter(table.Fields.get_Field(table.FindField(fildName)).Domain);
            cb.Items.AddRange(dda.ToArray());
            CheсkValueAndSetToComboBox(ref cb, ref dda, ref table, fildName, null);
        }
        //устоновить значение комбобокса по значению, если нет адаптера - создать, если нет значения устоновить по умолчанию
        public static void CheсkValueAndSetToComboBox(ref System.Windows.Forms.ComboBox cb, ref DomeinDataAdapter dda, ref ITable table, string fildName, object value)
        {
            if (dda == null)
            {
                GeneralApp.ShowErrorMessage("dda == null");
            }
            if (value == null || Convert.IsDBNull(value))
            {
                IField fild = table.Fields.get_Field(table.FindField(fildName));
                value = fild.DefaultValue;
                //костыль. почемуто значение по умолчанию имеет тип double, а тип домена и поля приетом short
                if (fild.Type == esriFieldType.esriFieldTypeSmallInteger && value is double)
                {
                    value = Convert.ToInt16(value);
                }
            }
            if ((value != null) && !Convert.IsDBNull(value))
                cb.SelectedIndex = dda.GetIndexByValue(value);
        }


        //одно значение домена
        public class DomeinData
        {
            //значение
            public readonly object Value;
            //наименование
            public readonly string Text;
            //внутренний индекс
            public readonly int Index;

            public DomeinData(int Index, object Value, string Text)
            {
                this.Index = Index;
                this.Value = Value;
                this.Text = Text;
            }
            public override string ToString()
            {
                return Text;
            }
        }

        //внутреннее представление
        private ArrayList data = null;
        //конструктор
        public DomeinDataAdapter(IDomain domain)
        {
            ICodedValueDomain codedValueDomain = (ICodedValueDomain)domain;
            if (codedValueDomain == null)
                return ;

            data = new ArrayList(codedValueDomain.CodeCount);

            for (int i = 0; i < codedValueDomain.CodeCount; i++)
            {
                data.Add(new DomeinData(i, codedValueDomain.get_Value(i), codedValueDomain.get_Name(i)));
            }
        }
        //вернуть массив значений
        public object[] ToArray()
        {
            if (data != null)
                return data.ToArray();
            else
                return null;
        }
        //вернуть значение по индексу
        public object GetValueByIndex(int index)
        {
            object ret = null;
            if (data != null && (index >=0 && index < data.Count ) )
                ret = ((DomeinData)data[index]).Value;
            
            return ret;
        }
        //вернуть текстовое наименование по индексу
        public string GetTextByIndex(int index)
        {
            string ret = "";
            if (data != null && (index >= 0 && index < data.Count))
                ret = ((DomeinData)data[index]).Text;

            return ret;
        }
        //вернуть значение по наименованию
        public object GetValueByText(string text)
        {
            return GetValueByIndex(GetIndexByText(text));
        }
        //вернуть наименование по значению
        public string GetTextByValue(object value)
        {
            return GetTextByIndex(GetIndexByValue(value));
        }
        //вернуть индекс по наименованию
        public int GetIndexByText(string text)
        {
            int ret = -1;
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if (((DomeinData)data[i]).Text.Equals(text))
                    {
                        ret = i;
                        break;
                    }
                }

            }
            return ret;
        }
        //вернуть индес по значению
        public int GetIndexByValue(object value)
        {
            int ret = -1;
            if (data != null)
            {
                for (int i = 0; i < data.Count; i++)
                {
                    if ( ((DomeinData)data[i]).Value.Equals(value) )
                    {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }
    }

}
