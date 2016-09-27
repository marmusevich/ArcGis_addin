
using System;
using System.Xml;
using System.Xml.Serialization;
using ESRI.ArcGIS.Display;
using ESRI.ArcGIS.esriSystem;

namespace CadastralReference
{
    /// <summary> Информация об одном листе
    /// </summary>
    [Serializable]
    public class OneTextElementDescription
    {
        #region свойсва / поля
        /// <summary> строка текстового элемента
        /// </summary>
        public string Text { get; set; }

        /// <summary> позиция по горизонтали
        /// </summary>
        public double PosX { get; set; }

        /// <summary>  позиция по вертикали
        /// </summary>
        public double PosY { get; set; }

        /// <summary>  точька отсчета на странице по горизонтали
        /// </summary>
        public esriTextHorizontalAlignment PagePosHorizontal { get; set; }

        /// <summary> точька отсчета на странице по вертикали
        /// </summary>
        public esriTextVerticalAlignment PagePosVertical { get; set; }

        /// <summary> точька привязки элемента по горизонтали - якорь
        /// </summary>
        public esriTextHorizontalAlignment AncorHorizontal { get; set; }

        /// <summary>  точька привязки элемента по вертикали - якорь
        /// </summary>
        public esriTextVerticalAlignment AncorVertical { get; set; }


        /// <summary> TextSymbolClass - дополнительные параметры элемента
        /// </summary>
        [XmlIgnore]
        ITextSymbol m_textSymbolClass = new TextSymbolClass();
        [XmlIgnore]
        public ITextSymbol TextSymbolClass 
        {
            get { return m_textSymbolClass; }
            set
            {
                if (m_textSymbolClass != value)
                {
                    m_textSymbolClass = value;
                    m_textSymbolClass_Serialized_innerUse = SerializeTextSymbolClassToByte(m_textSymbolClass);
                }
            }
        }

        /// <summary>  серелизованый TextSymbolClass для серелизации внутреннее использование
        /// </summary>
        [XmlIgnore]
        byte[] m_textSymbolClass_Serialized_innerUse;
        public byte[] TextSymbolClass_Serialized_innerUse 
        {
            get { return m_textSymbolClass_Serialized_innerUse; }
            set
            {
                if (m_textSymbolClass_Serialized_innerUse != value)
                {
                    m_textSymbolClass_Serialized_innerUse = (byte[])value.Clone();
                    m_textSymbolClass = DeSerializeByteToTextSymbolClass(m_textSymbolClass_Serialized_innerUse);
                }
            } 
        }
        #endregion

        // серелизовать в масив байтов
        private static byte[] SerializeTextSymbolClassToByte(ITextSymbol tsc)
        {
            if (tsc == null) return null;

            IXMLWriter xmlWriter = new XMLWriterClass();
            IXMLStream xmlStream = new XMLStreamClass();
            xmlWriter.WriteTo((IStream)xmlStream);
            IXMLSerializer xmlSerializer = new XMLSerializerClass();
            xmlSerializer.WriteObject(xmlWriter, null, null, "TextSymbolClass_Serialized", "OneTextElementDescription", tsc);
            return xmlStream.SaveToBytes();
        }
        // десерилезовать
        private static ITextSymbol DeSerializeByteToTextSymbolClass(byte[] byteArr)
        {
            if (byteArr == null) return null;

            IXMLStream xmlStream = new XMLStreamClass();
            xmlStream.LoadFromBytes(byteArr);
            IXMLReader xmlReader = new XMLReaderClass();
            xmlReader.ReadFrom((IStream)xmlStream); // Explicit Cast
            IXMLSerializer xmlSerializer = new XMLSerializerClass();
            return (ITextSymbol)xmlSerializer.ReadObject(xmlReader, null, null);
        }


        public override string ToString()
        {
            return Text;
        }

        public OneTextElementDescription()
        {
            this.Text = "";
            this.PosX = 0;
            this.PosY = 0;
            this.PagePosHorizontal = esriTextHorizontalAlignment.esriTHALeft;
            this.PagePosVertical = esriTextVerticalAlignment.esriTVABottom;
            this.AncorHorizontal = esriTextHorizontalAlignment.esriTHALeft;
            this.AncorVertical = esriTextVerticalAlignment.esriTVABottom;
            this.m_textSymbolClass = new TextSymbolClass();
            this.m_textSymbolClass_Serialized_innerUse = SerializeTextSymbolClassToByte(m_textSymbolClass);
        }

        //скопировать настройки
        public void CopySetingFrom(OneTextElementDescription oted)
        {
            this.Text = oted.Text;
            this.PosX = oted.PosX;
            this.PosY = oted.PosY;
            this.PagePosHorizontal = oted.PagePosHorizontal;
            this.PagePosVertical = oted.PagePosVertical;
            this.AncorHorizontal = oted.AncorHorizontal;
            this.AncorVertical = oted.AncorVertical;
            this.m_textSymbolClass_Serialized_innerUse = (byte[])oted.TextSymbolClass_Serialized_innerUse.Clone();
            this.m_textSymbolClass = DeSerializeByteToTextSymbolClass(m_textSymbolClass_Serialized_innerUse);
        }
    }
}
