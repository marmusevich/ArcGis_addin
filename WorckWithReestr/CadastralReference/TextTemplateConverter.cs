using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CadastralReference
{
    // замена шаблонных переменных, и реализация замены
    public static class TextTemplateConverter
    {
        #region описание методов замены

        //интерфейс
        interface IOneTextTemplateConverter
        {
            string GeVariableName();
            string GeDiscription();
            string GetReplesedString();
        }
        // маштаб карты
        class MapScale : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_MapScale_}";
            }
            public string GeDiscription()
            {
                return "текущий маштаб карты в формате 1:XXX";
            }
            public string GetReplesedString()
            {
                return "1:" + Math.Round(WorkCadastralReference_MAP.GetShowMapScale()).ToString();
            }
        }
        class AdressOpisatelniy : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_ОписательныйАдрес_}";
            }
            public string GeDiscription()
            {
                return "адресс из заявки";
            }
            public string GetReplesedString()
            {
                return "адресс из заявки";
            }
        }


        #endregion

        static IOneTextTemplateConverter[] arrMetod = new IOneTextTemplateConverter[] 
        { new MapScale(), new AdressOpisatelniy()};

        // произвести замену
        public static string Implement(string input)
        {
            StringBuilder sb = new StringBuilder(input);
            foreach (IOneTextTemplateConverter ottc in arrMetod)
            {
                sb.Replace(ottc.GeVariableName(), ottc.GetReplesedString());
            }
            return sb.ToString();       
        }

        //вернуть строку описание переменных шаблона
        public static string GetDiscription()
        {
            StringBuilder sb = new StringBuilder();
            foreach (IOneTextTemplateConverter ottc in arrMetod)
            {
                sb.Append(ottc.GeVariableName());
                sb.Append(" - ");
                sb.Append(ottc.GeDiscription());
                sb.Append("\n");
            }
            return sb.ToString();
        }
    }
}
