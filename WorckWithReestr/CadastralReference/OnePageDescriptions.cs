using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;
using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.esriSystem;
using ESRI.ArcGIS.Display;
using SharedClasses;

namespace CadastralReference
{
    /// <summary>
    /// Информация об одном листе
    /// </summary>
    [Serializable]
    public class OnePageDescriptions : IEquatable<OnePageDescriptions>
    {
        #region свойсва / поля
        /// <summary>
        ///  имя из базы Данных для поля 
        /// </summary>
        [XmlIgnore]
        public int PagesID { get; private set; }
        /// <summary>
        /// Назвение листа
        /// </summary>
        [XmlElement("Caption")]
        public string Caption
        {
            get
            {
                return m_Caption;
            }
            set
            {
                if (m_Caption != value)
                {
                    m_Caption = value;
                    PagesID = m_Caption.GetHashCode();
                }
            }
        }
        string m_Caption;
        /// <summary>
        /// включен ли лист
        /// </summary>
        [XmlElement("Enable", Type = typeof(bool))]
        public bool Enable { get; set; }
        /// <summary>
        ///  Слои этого листа
        /// </summary>
        [XmlArray("Layers"), XmlArrayItem("Layer")]
        public StringCollection Layers { get; set; }
        /// <summary>
        /// макет 
        /// </summary>
        [XmlIgnore]
        public Image Image
        {
            get
            {
                return m_Image;
            }
            set
            {
                if (m_Image != value)
                {
                    m_Image = value;
                    if (Image_Change != null)
                        Image_Change(this, EventArgs.Empty);
                }
            }
        }
        private Image m_Image = null;

        /// <summary>
        /// описание текстовых элементов на листе
        /// </summary>
        [XmlArray("TextElements"), XmlArrayItem("TextElement")]
        public List<OneTextElementDescription> TextElements { get { return m_TextElements; } set { m_TextElements = value; } }
        private List<OneTextElementDescription> m_TextElements;

        // размер фрейма данных
        [XmlElement("DataFrameSyze_Down", Type = typeof(double))]
        public double DataFrameSyze_Down = 0;
        [XmlElement("DataFrameSyze_Up", Type = typeof(double))]
        public double DataFrameSyze_Up = 0;
        [XmlElement("DataFrameSyze_Left", Type = typeof(double))]
        public double DataFrameSyze_Left = 0;
        [XmlElement("DataFrameSyze_Right", Type = typeof(double))]
        public double DataFrameSyze_Right = 0;

        // ручной маштаб
        public double Scale_Manual = 0;
        // режим маштобирования
        public int ScaleMode = 0;


        // стрелка севера
        [XmlElement("IsHasNorthArrow", Type = typeof(bool))]
        public bool IsHasNorthArrow = false;
        [XmlElement("NorthArrow_PosX", Type = typeof(double))]
        public double NorthArrow_PosX { get; set; }
        [XmlElement("NorthArrow_PosY", Type = typeof(double))]
        public double NorthArrow_PosY { get; set; }
        /// <summary>  точька отсчета на странице по горизонтали
        /// </summary>
        public esriTextHorizontalAlignment NorthArrow_PagePosHorizontal { get; set; }
        /// <summary> точька отсчета на странице по вертикали
        /// </summary>
        public esriTextVerticalAlignment NorthArrow_PagePosVertical { get; set; }
        [XmlIgnore]
        INorthArrow m_NorthArrow = null;
        [XmlIgnore]
        public INorthArrow NorthArrow
        {
            get { return m_NorthArrow; }
            set
            {
                if (m_NorthArrow != value)
                {
                    m_NorthArrow = value;
                    m_NorthArrow_Serialized_innerUse = SerializeNorthArrowToByte(m_NorthArrow);
                }
            }
        }
        /// <summary>  серелизованый INorthArrow для серелизации внутреннее использование
        /// </summary>
        [XmlIgnore]
        byte[] m_NorthArrow_Serialized_innerUse = null;
        public byte[] NorthArrow_Serialized_innerUse
        {
            get { return m_NorthArrow_Serialized_innerUse; }
            set
            {
                if (m_NorthArrow_Serialized_innerUse != value)
                {
                    m_NorthArrow_Serialized_innerUse = (byte[])value.Clone();
                    m_NorthArrow = DeSerializeByteToNorthArrow(m_NorthArrow_Serialized_innerUse);
                }
            }
        }



        [XmlElement("IsHasScaleBar", Type = typeof(bool))]
        public bool IsHasScaleBar = false;
        [XmlElement("ScaleBar_PosX", Type = typeof(double))]
        public double ScaleBar_PosX { get; set; }
        [XmlElement("ScaleBar_PosY", Type = typeof(double))]
        public double ScaleBar_PosY { get; set; }
        [XmlElement("ScaleBar_Height", Type = typeof(double))]
        public double ScaleBar_Height { get; set; }
        [XmlElement("ScaleBar_Width", Type = typeof(double))]
        public double ScaleBar_Width { get; set; }
        /// <summary>  точька отсчета на странице по горизонтали
        /// </summary>
        public esriTextHorizontalAlignment ScaleBar_PagePosHorizontal { get; set; }
        /// <summary> точька отсчета на странице по вертикали
        /// </summary>
        public esriTextVerticalAlignment ScaleBar_PagePosVertical { get; set; }
        /// <summary> точька привязки элемента по горизонтали - якорь
        /// </summary>
        public esriTextHorizontalAlignment ScaleBar_AncorHorizontal { get; set; }
        /// <summary>  точька привязки элемента по вертикали - якорь
        /// </summary>
        public esriTextVerticalAlignment ScaleBar_AncorVertical { get; set; }
        public string TypeScaleBarName = "";
        [XmlIgnore]
        IScaleBar m_ScaleBar = null;
        [XmlIgnore]
        public IScaleBar ScaleBar
        {
            get { return m_ScaleBar; }
            set
            {
                if (m_ScaleBar != value)
                {
                    m_ScaleBar = value;
                    TypeScaleBarName = m_ScaleBar.Name;
                    m_ScaleBar_Serialized_innerUse = SerializeScaleBarToByte(m_ScaleBar);
                }
            }
        }
        /// <summary>  серелизованый INorthArrow для серелизации внутреннее использование
        /// </summary>
        [XmlIgnore]
        byte[] m_ScaleBar_Serialized_innerUse = null;
        public byte[] ScaleBar_Serialized_innerUse
        {
            get { return m_ScaleBar_Serialized_innerUse; }
            set
            {
                if (m_ScaleBar_Serialized_innerUse != value)
                {
                    m_ScaleBar_Serialized_innerUse = (byte[])value.Clone();
                    m_ScaleBar = DeSerializeByteToScaleBar(m_ScaleBar_Serialized_innerUse, TypeScaleBarName);
                }
            }
        }
        #endregion

        #region события
        /// <summary>
        /// смена изображения
        /// </summary>
        public event EventHandler<EventArgs> Image_Change;
        #endregion

        #region перегрузка стандартных функций
        public override string ToString()
        {
            return Caption;
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            OnePageDescriptions objAsPart = obj as OnePageDescriptions;
            if (objAsPart == null) return false;
            else return Equals(objAsPart);
        }
        public override int GetHashCode()
        {
            return PagesID;
        }
        public bool Equals(OnePageDescriptions other)
        {
            if (other == null) return false;
            return (this.PagesID.Equals(other.PagesID));
        }
        #endregion


        /// <summary>
        /// иницилизация 
        /// </summary>
        /// <param name="caption"> название </param>
        /// <param name="enable"> включен ли?</param>
        public OnePageDescriptions(string caption, bool enable = false):this()
        {
            Caption = caption;
            Enable = enable;
        }

        public OnePageDescriptions()
        {
            Caption = "";
            Enable = false;
            Layers = new StringCollection();
            m_Image = null;
            m_TextElements = new List<OneTextElementDescription>();

            m_NorthArrow = new MarkerNorthArrow();
            m_NorthArrow_Serialized_innerUse = SerializeNorthArrowToByte(m_NorthArrow);
            NorthArrow_PagePosHorizontal = esriTextHorizontalAlignment.esriTHALeft;
            NorthArrow_PagePosVertical = esriTextVerticalAlignment.esriTVABottom;

            m_ScaleBar = new ScaleLine();
            m_ScaleBar.Units = esriUnits.esriKilometers;
            TypeScaleBarName = "Scale Line";
            m_ScaleBar_Serialized_innerUse = SerializeScaleBarToByte(m_ScaleBar);
            ScaleBar_PagePosHorizontal = esriTextHorizontalAlignment.esriTHARight;
            ScaleBar_PagePosVertical = esriTextVerticalAlignment.esriTVABottom;
            ScaleBar_AncorHorizontal = esriTextHorizontalAlignment.esriTHARight;
            ScaleBar_AncorVertical = esriTextVerticalAlignment.esriTVABottom;
            ScaleBar_PosX = 0;
            ScaleBar_PosY = 0;
            ScaleBar_Height = 0.75;
            ScaleBar_Width = 6.65;
    }

    //скопировать настройки
    public void CopySetingFrom(OnePageDescriptions opd)
        {
            this.Caption = opd.Caption;
            this.Enable = opd.Enable;
            Layers = new StringCollection();
            foreach (string s in opd.Layers)
                this.Layers.Add(s);

            m_TextElements = new List<OneTextElementDescription>();
            foreach (OneTextElementDescription oted in opd.TextElements)
            {
                OneTextElementDescription tmp = new OneTextElementDescription();
                tmp.CopySetingFrom(oted);
                this.m_TextElements.Add(tmp);
            }

            this.DataFrameSyze_Down = opd.DataFrameSyze_Down;
            this.DataFrameSyze_Up = opd.DataFrameSyze_Up;
            this.DataFrameSyze_Left = opd.DataFrameSyze_Left;
            this.DataFrameSyze_Right = opd.DataFrameSyze_Right;

            this.IsHasNorthArrow = opd.IsHasNorthArrow;
            this.NorthArrow_PosX = opd.NorthArrow_PosX;
            this.NorthArrow_PosY = opd.NorthArrow_PosY;
            this.NorthArrow_PagePosHorizontal = opd.NorthArrow_PagePosHorizontal;
            this.NorthArrow_PagePosVertical = opd.NorthArrow_PagePosVertical;
            this.m_NorthArrow_Serialized_innerUse = (byte[])opd.m_NorthArrow_Serialized_innerUse.Clone();
            this.m_NorthArrow = DeSerializeByteToNorthArrow(m_NorthArrow_Serialized_innerUse);

            this.IsHasScaleBar = opd.IsHasScaleBar;
            this.TypeScaleBarName = opd.TypeScaleBarName;
            this.ScaleBar_PosX = opd.ScaleBar_PosX;
            this.ScaleBar_PosY = opd.ScaleBar_PosY;
            this.ScaleBar_PagePosHorizontal = opd.ScaleBar_PagePosHorizontal;
            this.ScaleBar_PagePosVertical = opd.ScaleBar_PagePosVertical;
            this.ScaleBar_Height = opd.ScaleBar_Height;
            this.ScaleBar_Width = opd.ScaleBar_Width;
            this.ScaleBar_AncorHorizontal = opd.ScaleBar_AncorHorizontal;
            this.ScaleBar_AncorVertical = opd.ScaleBar_AncorVertical;
            this.m_ScaleBar_Serialized_innerUse = (byte[])opd.m_ScaleBar_Serialized_innerUse.Clone();
            this.m_ScaleBar = DeSerializeByteToScaleBar(m_ScaleBar_Serialized_innerUse, TypeScaleBarName);

            this.Scale_Manual = opd.Scale_Manual;
            this.ScaleMode = opd.ScaleMode;

        }

        //предстовленеие перечня слоев
        public string LayersToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            foreach (string s in Layers)
            {
                sb.Append("[");
                sb.Append(s);
                sb.Append("] ");
            }
            string str = sb.ToString();
            if (str == "") str = "Не указаны.";

            return str;
        }

        // серелизовать в масив байтов стрелку севера
        private static byte[] SerializeNorthArrowToByte(INorthArrow northArrow)
        {
            if (northArrow == null) return null;
            IXMLStream xmlStream = new XMLStreamClass();
            ((ESRI.ArcGIS.esriSystem.IPersistStream)northArrow).Save((IStream)xmlStream, 0);
            return xmlStream.SaveToBytes();
        }
        // десерилезовать стрелку севера
        private static INorthArrow DeSerializeByteToNorthArrow(byte[] byteArr)
        {
            if (byteArr == null) return null;
            MarkerNorthArrow northArrow = new MarkerNorthArrow();
            try
            {
                IXMLStream xmlStream = new XMLStreamClass();
                xmlStream.LoadFromBytes(byteArr);
                ((IPersistStream)northArrow).Load((IStream)xmlStream);
            }
            catch (Exception ex) // обработка ошибок
            {

                Logger.Write(ex, string.Format(" DeSerializeByteToNorthArrow "));
            }
            return northArrow;
        }


        // серелизовать в масив байтов стрелку севера
        private static byte[] SerializeScaleBarToByte(IScaleBar ScaleBar)
        {
            if (ScaleBar == null) return null;
            IXMLStream xmlStream = new XMLStreamClass();
            ((ESRI.ArcGIS.esriSystem.IPersistStream)ScaleBar).Save((IStream)xmlStream, 0);
            return xmlStream.SaveToBytes();
        }
        // десерилезовать стрелку севера
        private static IScaleBar DeSerializeByteToScaleBar(byte[] byteArr, string TypeScaleBarName)
        {
            if (byteArr == null) return null;
            IScaleBar ScaleBar = null;
            try
            {
                IXMLStream xmlStream = new XMLStreamClass();
                xmlStream.LoadFromBytes(byteArr);

                if (TypeScaleBarName == "Alternating Scale Bar")
                    ScaleBar = new AlternatingScaleBar();
                if (TypeScaleBarName == "Double Alternating Scale Bar")
                    ScaleBar = new DoubleAlternatingScaleBar();
                if (TypeScaleBarName == "Hollow Scale Bar")
                    ScaleBar = new HollowScaleBar();
                if (TypeScaleBarName == "Scale Line")
                    ScaleBar = new ScaleLine();
                if (TypeScaleBarName == "Single Division Scale Bar")
                    ScaleBar = new SingleDivisionScaleBar();
                if (TypeScaleBarName == "Stepped Scale Line")
                    ScaleBar = new SteppedScaleLine();

                if (ScaleBar == null) return null;

                ((IPersistStream)ScaleBar).Load((IStream)xmlStream);
            }
            catch (Exception ex) // обработка ошибок
            {

                Logger.Write(ex, string.Format(" DeSerializeByteToScaleBar "));
            }
            return ScaleBar;
        }
    }
}
