using System.Collections;
using ESRI.ArcGIS.Geodatabase;


namespace WorckWithReestr
{
    //обертка над перечисляемыми доменнами
    class DomeinDataAdapter
    {
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
            return GetTextByValue(GetIndexByValue(value));
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
                    if( ((DomeinData)data[i]).Value.Equals(value) )
                    {
                        ret = i;
                        break;
                    }
                }
            }
            return ret;
        }    
    }
    //одно значение домена
    class DomeinData
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
            return this.Text;
        }
    }


}
