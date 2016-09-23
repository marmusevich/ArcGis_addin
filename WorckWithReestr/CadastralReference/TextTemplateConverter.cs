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
                return "{_маштаб_}";
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
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["Adress_Text"] as string;
                }
                return ret;
            }
        }
        class NomerZayavki : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_НомерЗаявки_}";
            }
            public string GeDiscription()
            {
                return "номер заявки";
            }
            public string GetReplesedString()
            {
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["N_Z"] as string;
                }
                return ret;
            }
        }
        class DataZayavki : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_ДатаЗаявки_}";
            }
            public string GeDiscription()
            {
                return "дата заявки";
            }
            public string GetReplesedString()
            {
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = ((DateTime)WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["Data_Z"]).ToString();
                }
                return ret;
            }
        }
        class Zayavitel : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_Заявитель_}";
            }
            public string GeDiscription()
            {
                return "название заявителя";
            }
            public string GetReplesedString()
            {
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["strKod_Z"] as string;
                }
                return ret;
            }
        }
        class KancelyarskiyNomer : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_ВходящийНомер_}";
            }
            public string GeDiscription()
            {
                return "номер входящий";
            }
            public string GetReplesedString()
            {
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["Cane"] as string;
                }
                return ret;
            }
        }
        class KancelyarskiyData : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_ВходящаяДата_}";
            }
            public string GeDiscription()
            {
                return "дата входящая";
            }
            public string GetReplesedString()
            {
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = ((DateTime)WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["Cane_Date"]).ToString();
                }
                return ret;
            }
        }
        class Rajon : IOneTextTemplateConverter
        {
            public string GeVariableName()
            {
                return "{_Район_}";
            }
            public string GeDiscription()
            {
                return "район города";
            }
            public string GetReplesedString()
            {
                string ret = "";
                if (WorkCadastralReference.GetCadastralReferenceData().ZayavkaData != null)
                {
                    ret = WorkCadastralReference.GetCadastralReferenceData().ZayavkaData["strRajon"] as string;
                }
                return ret;
            }
        }
        #endregion

        static IOneTextTemplateConverter[] arrMetod = new IOneTextTemplateConverter[] 
        { new MapScale(), new AdressOpisatelniy(), new NomerZayavki(), new DataZayavki(), new Zayavitel(),
          new KancelyarskiyNomer(), new KancelyarskiyData(), new Rajon()};

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
