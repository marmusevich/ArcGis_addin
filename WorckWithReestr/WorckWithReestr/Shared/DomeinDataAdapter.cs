using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;


namespace WorckWithReestr
{

    //вспомогательный для выбора значений
    class DomeinData
    {
        public readonly object Value;
        public readonly string Text;
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



    class DomeinDataAdapter
    {
        private ArrayList data = null;

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

        public object[] ToArray()
        {
            if (data != null)
                return data.ToArray();
            else
                return null;
        }

        public object GetValueByIndex(int index)
        {
            object ret = null;
            if (data != null && (index >=0 && index < data.Count ) )
                ret = ((DomeinData)data[index]).Value;
            
            return ret;
        }
        
        public string GetTextByIndex(int index)
        {
            string ret = "";
            if (data != null && (index >= 0 && index < data.Count))
                ret = ((DomeinData)data[index]).Text;

            return ret;
        }

        public object GetValueByText(string text)
        {
            return GetValueByIndex(GetIndexByText(text));
        }

        public string GetTextByValue(object value)
        {
            return GetTextByValue(GetIndexByValue(value));
        }

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
}
