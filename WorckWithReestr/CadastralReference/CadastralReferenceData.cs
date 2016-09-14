using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CadastralReference
{
    //CadastralReference
    public class CadastralReferenceData
    {
        #region // для теста
        private static string[] m_ArrayDBNamesPages = new string[6]
                  {
                    "Выкопировка с топологии",
                    "Генеральный план",
                    "Генеральный план: планировочные ограничения",
                    "Детальный план: схема зонирования територий",
                    "Детальный план: схема планировочные ограничения",
                    "Граници участков смежных землепользователей"
                  };
        #endregion // для теста


        public void InitPagesDescription()
        {
            m_Pages = new List<OnePageDescriptions>();
            foreach (string str in m_ArrayDBNamesPages)
            {
                OnePageDescriptions opd = new OnePageDescriptions(str, true);
                opd.Image_Change += new EventHandler<EventArgs>(OnImage_Change);
                m_Pages.Add(opd);
            }
        }


        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region внутренние переменные
        List<OnePageDescriptions> m_Pages = null;

        private int m_ZayavkaID = -1;
        private int m_ObjektInMapID = -1;
        private string m_AllRTF = "";
        private string m_TitulRTF_Template = " Титул ";
        private string m_Page1RTF_Template = " начало динамической части";
        private StringCollection m_DinamicRTF_Template = null;
        private StringCollection m_DinamicRTF = null;
        private string m_ConstRTF_Template = " постоянная часть";
        private string m_RaspiskaRTF_Template = " расписка";
        private string m_ObjectLayerName = "Кадастровая_справка.DBO.KS_OBJ_FOR_ALEX"; // проблема, это не имя слоя
        private Dictionary<string, object> m_ZayavkaData = null;
        #endregion

        /// ////////////////////////////////////////////////////////////////////////////////////////////
        #region публичные свойства
        /// <summary>
        /// свойства графических листов
        /// </summary>
        public List<OnePageDescriptions> Pages { get { return m_Pages; } private set { m_Pages = value; } }
        /// <summary>
        /// код заявления
        /// </summary>
        public int ZayavkaID 
        { 
            get 
            { 
                return m_ZayavkaID; 
            } 
            set 
            {
                if(m_ZayavkaID != value)
                { 
                m_ZayavkaID = value;
                if (ZayavkaID_Change != null)
                    ZayavkaID_Change(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// код объекта
        /// </summary>
        public int ObjektInMapID
        {
            get
            {
                return m_ObjektInMapID;
            }
            set
            {
                if (m_ObjektInMapID != value)
                {
                    m_ObjektInMapID = value;
                    if (ObjektInMapID_Change != null)
                        ObjektInMapID_Change(this, EventArgs.Empty);
                }
            }
        }
        /// <summary>
        /// весь текст итогового документа
        /// </summary>
        public string AllRTF { get { return m_AllRTF; } set { m_AllRTF = value; } }
        /// <summary>
        /// титульный лист - шаблон
        /// </summary>
        public string TitulRTF_Template { get { return m_TitulRTF_Template; } set { m_TitulRTF_Template = value; } }
        /// <summary>
        /// надпись до динамической части - шаблон
        /// </summary>
        public string Page1RTF_Template { get { return m_Page1RTF_Template; } set { m_Page1RTF_Template = value; } }
        /// <summary>
        /// Динамическая часть - шаблон
        /// </summary>
        public StringCollection DinamicRTF_Template { get { return m_DinamicRTF_Template; } set { m_DinamicRTF_Template = value; } }
        /// <summary>
        /// Динамическая часть из документа
        /// </summary>
        public StringCollection DinamicRTF { get { return m_DinamicRTF; } set { m_DinamicRTF = value; } }
        /// <summary>
        /// окончание документа - шаблон
        /// </summary>
        public string ConstRTF_Template { get { return m_ConstRTF_Template; } set { m_ConstRTF_Template = value; } }
        /// <summary>
        /// расписка - шаблон
        /// </summary>
        public string RaspiskaRTF_Template { get { return m_RaspiskaRTF_Template; } set { m_RaspiskaRTF_Template = value; } }
        /// <summary>
        /// имя слоя на котором размещены объекты
        /// </summary>
        public string ObjectLayerName { get { return m_ObjectLayerName; } set { m_ObjectLayerName = value; } }
        /// <summary>
        /// справка закрыта для редактирования
        /// </summary>
        public bool IsCloseToEdit { get; set; }
        /// <summary>
        /// Данные заявки
        /// </summary>
        public Dictionary<string, object> ZayavkaData { get { return m_ZayavkaData; } set { m_ZayavkaData = value; } }

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region функции

        #endregion

        ////////////////////////////////////////////////////////////////////////////////////////////
        #region события
        /// <summary>
        /// смена заявления
        /// </summary>
        public event EventHandler<EventArgs> ZayavkaID_Change;
        /// <summary>
        /// смена объекта
        /// </summary>
        public event EventHandler<EventArgs> ObjektInMapID_Change;
        /// <summary>
        ///смена изображения
        /// </summary>
        public event EventHandler<EventArgs> Image_Change;
        private void OnImage_Change(object sender, EventArgs e)
        {
            if (Image_Change != null)
                Image_Change(sender, EventArgs.Empty);
        }
        #endregion
    }
}




//// дерево развития в игре, прототип


//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Text;
//using System.Xml;
//using System.Xml.Serialization;

//// добавить проверки

//namespace TreeDevolopment
//{



//    // собственно классс
//    namespace TestTreeDevelopment
//    {
//        public class TreeDevelopment
//        {
//            // внутреннее представление
//            public class TreeDevelopmentElement
//            {
//                //зависит
//                public List<string> mDepends = null;
//                //влияет
//                public List<string> mEffectOn = null;
//                //стоимость изучения
//                public int mTrainingPrice = 0;
//                //изучено
//                public bool mIsStudied = false;
//                //может быть изучено
//                public bool mIsCanStudied = false;

//                public TreeDevelopmentElement()
//                {
//                    mDepends = new List<string>();
//                    mEffectOn = new List<string>();
//                }

//                public void SetDepend(string key)
//                {
//                    mDepends.Remove(key);
//                    if (mDepends.Count() == 0)
//                        mIsCanStudied = true;
//                }
//            }
//            //хранилище
//            private Dictionary<string, TreeDevelopmentElement> mData = null;

//            public TreeDevelopment()
//            {
//                mData = new Dictionary<string, TreeDevelopmentElement>();
//            }

//            //добавить
//            public void Add(string Key, int TrainingPrice)
//            {
//                TreeDevelopmentElement tmp = new TreeDevelopmentElement();
//                tmp.mTrainingPrice = TrainingPrice;
//                mData.Add(Key, tmp);
//            }

//            // установить зависемость, Key - этот ключь влияет на KeyEffectOn
//            public void SetDepend(string Key, string KeyEffectOn)
//            {
//                TreeDevelopmentElement tmp;
//                tmp = mData[KeyEffectOn];
//                tmp.mDepends.Add(Key);

//                tmp = mData[Key];
//                tmp.mEffectOn.Add(KeyEffectOn);
//            }

//            //выучить
//            public void SetStudie(string key)
//            {
//                TreeDevelopmentElement tmp = mData[key];
//                tmp.mIsStudied = true;

//                foreach (string effectOnKey in tmp.mEffectOn)
//                {
//                    TreeDevelopmentElement effect = mData[effectOnKey];
//                    effect.SetDepend(key);
//                }
//            }


//            //печать
//            public void Print()
//            {
//                foreach (KeyValuePair<string, TreeDevelopmentElement> tmp in mData)
//                {
//                    string str = string.Format("{0}\t Is Can Studied={1},\t Is Studied={2},\t Training Price={3}", tmp.Key, tmp.Value.mIsCanStudied, tmp.Value.mIsStudied, tmp.Value.mTrainingPrice);
//                    System.Console.WriteLine(str);
//                    str = "";
//                    foreach (string s in tmp.Value.mDepends)
//                        str += s + ", ";
//                    System.Console.WriteLine("\t Depends - " + str);
//                    str = "";
//                    foreach (string s in tmp.Value.mEffectOn)
//                        str += s + ", ";
//                    System.Console.WriteLine("\t EffectOn - " + str);
//                }
//            }

//            public void Save(string fn)
//            {
//                XmlWriterSettings settings = new XmlWriterSettings();
//                settings.Indent = true;
//                settings.IndentChars = "\t";
//                using (XmlWriter writer = XmlTextWriter.Create(fn, settings))
//                {
//                    writer.WriteStartElement("root");
//                    foreach (KeyValuePair<string, TreeDevelopmentElement> tmp in mData)
//                    {
//                        writer.WriteStartElement("Key");
//                        writer.WriteAttributeString("IsCanStudied", tmp.Value.mIsCanStudied.ToString());
//                        writer.WriteAttributeString("IsStudied", tmp.Value.mIsStudied.ToString());
//                        writer.WriteAttributeString("TrainingPrice", tmp.Value.mTrainingPrice.ToString());
//                        writer.WriteString(tmp.Key);
//                        foreach (string s in tmp.Value.mDepends)
//                        {
//                            writer.WriteStartElement("Depend");
//                            writer.WriteString(s);
//                            writer.WriteEndElement();
//                        }

//                        foreach (string s in tmp.Value.mEffectOn)
//                        {
//                            writer.WriteStartElement("EffectOn");
//                            writer.WriteString(s);
//                            writer.WriteEndElement();
//                        }

//                        writer.WriteEndElement();
//                    }
//                    writer.WriteEndElement();
//                }
//            }

//            public void Load(string fn)
//            {
//                try
//                {
//                    if (File.Exists(fn))
//                    {
//                        XmlDocument doc = new XmlDocument();
//                        doc.Load(fn);

//                        XmlNodeList elemList = doc.GetElementsByTagName("Key");
//                        for (int i = 0; i < elemList.Count; i++)
//                        {
//                            XmlNode node = elemList[i];

//                            TreeDevelopmentElement tmp = new TreeDevelopmentElement();
//                            tmp.mIsCanStudied = Boolean.Parse(node.Attributes["IsCanStudied"].Value);
//                            tmp.mIsStudied = Boolean.Parse(node.Attributes["IsStudied"].Value);
//                            tmp.mTrainingPrice = int.Parse(node.Attributes["TrainingPrice"].Value);

//                            mData.Add(node.FirstChild.InnerText, tmp);

//                        }

//                        elemList = doc.GetElementsByTagName("Key");
//                        for (int i = 0; i < elemList.Count; i++)
//                        {
//                            XmlNode node = elemList[i];
//                            XmlNodeList innerElemList = node.ChildNodes;
//                            for (int j = 0; j < innerElemList.Count; j++)
//                            {
//                                XmlNode innNode = innerElemList[j];
//                                if (innNode.FirstChild != null)
//                                {
//                                    if (innNode.Name == "Depend")
//                                        mData[node.FirstChild.InnerText].mDepends.Add(innNode.FirstChild.InnerText);
//                                    if (innNode.Name == "EffectOn")
//                                        mData[node.FirstChild.InnerText].mEffectOn.Add(innNode.FirstChild.InnerText);
//                                }
//                            }
//                        }
//                    }
//                }
//                catch (Exception ex) // обработка ошибок
//                {
//                    System.Console.WriteLine(string.Format("{0}.{1}()] {2}\r\n", ex.TargetSite.DeclaringType, ex.TargetSite.Name, ex.Message));
//                }
//            }
//        }
//    }
//*/


//    /* -- test for this class
//namespace TestTreeDevelopment
//{
//	class Program
//	{
//		static void Main(string[] args)
//		{
//			string fn = "m:/_GitHub/c#/TestTreeDevelopment/TestTreeDevelopment/TreeDevelopment.xml";
//			string fn2 = "m:/_GitHub/c#/TestTreeDevelopment/TestTreeDevelopment/TreeDevelopment2.xml";
			
			
//			TreeDevelopment td = new TreeDevelopment();
//			// Init
//			td.Add("Base1", 0);
//			td.Add("Base2", 0);
//			td.Add("der1", 100);
//			td.Add("der2", 150);
//			td.Add("der3", 170);
//			td.Add("der4", 180);
			
//			//SetDepend
//			td.SetDepend("Base1",   "der1");
//			td.SetDepend("der1",    "der2");
//			td.SetDepend("Base2",   "der2");
//			td.SetDepend("der2",    "der3");
//			td.SetDepend("der2",    "der4");
			
//			//td.Print();
			
//			//System.Console.WriteLine("\nSetStudie ->  Base1, Base2" );
//			//td.SetStudie("Base1");
//			//td.SetStudie("Base2");
//			////td.Print();
			
//			//System.Console.WriteLine("\nSetStudie ->  der1" );
//			//td.SetStudie("der1");
//			////td.Print();
			
			
//			td.Save(fn);
			
			
//			System.Console.WriteLine("\n\n\n -----  TD1  ------");
//			TreeDevelopment td1 = new TreeDevelopment();
//			td1.Load(fn);
//			td1.Print();
			
//			td1.Save(fn2);
			
			
//			System.Console.ReadLine();
//		}
//	}
//}
//*/
//}
