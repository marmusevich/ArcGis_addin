
using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Serialization;

namespace CadastralReference
{

    //     Horizontal text alignment options.
    public enum esriTextHorizontalAlignment
    {
        //
        // Сводка:
        //     The text is left justified.
        esriTHALeft = 0,
        //
        // Сводка:
        //     The text is center justified.
        esriTHACenter = 1,
        //
        // Сводка:
        //     The text is right justified.
        esriTHARight = 2,
        //
        // Сводка:
        //     The text is fully justified.
        esriTHAFull = 3
    }

    //     Vertical text alignment options.
    public enum esriTextVerticalAlignment
    {
        //
        // Сводка:
        //     The text is aligned at the top.
        esriTVATop = 0,
        //
        // Сводка:
        //     The text is aligned at the center.
        esriTVACenter = 1,
        //
        // Сводка:
        //     The text is aligned at the baseline.
        esriTVABaseline = 2,
        //
        // Сводка:
        //     The text is aligned at the bottom.
        esriTVABottom = 3
    }



    public class TextSymbolClass
    { }

    


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
        TextSymbolClass m_textSymbolClass = new TextSymbolClass();
        [XmlIgnore]
        public TextSymbolClass TextSymbolClass 
        {
            get { return m_textSymbolClass; }
            set
            {
                if (m_textSymbolClass != value)
                {
                    m_textSymbolClass = value;
                    m_textSymbolClass_Serialized_innerUse = SerializTextSymbolClassToByte(m_textSymbolClass);
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
                    m_textSymbolClass = DeSerializByteToTextSymbolClass(m_textSymbolClass_Serialized_innerUse);
                }
            } 
        }
        //ITextSymbol symbol

        #endregion

        // серелизовать в масив байтов
        private static byte[] SerializTextSymbolClassToByte(TextSymbolClass tsc)
        {
            return null;
        }
        // десерилезовать
        private static TextSymbolClass DeSerializByteToTextSymbolClass(byte[] byteArr)
        {
            return null;
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
            this.m_textSymbolClass_Serialized_innerUse = new byte[1];
            this.m_textSymbolClass = null;
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
            this.m_textSymbolClass = DeSerializByteToTextSymbolClass(m_textSymbolClass_Serialized_innerUse);
        }

    }
}
