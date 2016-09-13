using System;
using System.Drawing;
using System.Collections.Generic;
using System.Collections.Specialized;

namespace CadastralReference
{
    /// <summary>
    /// Информация об одном листе
    /// </summary>
    public class OnePageDescriptions : IEquatable<OnePageDescriptions>
    {
        /// <summary>
        /// иницилизация 
        /// </summary>
        /// <param name="caption"> название </param>
        /// <param name="enable"> включен ли?</param>
        public OnePageDescriptions(string caption, bool enable = false)
        {
            Caption = caption;
            Enable = enable;
            PagesID = Caption.GetHashCode();
            Layers = new StringCollection();
            m_Image = null;
        }

        #region свойсва / поля
        /// <summary>
        ///  имя из базы Данных для поля 
        /// </summary>
        public int PagesID { get; private set; }
        /// <summary>
        /// Назвение листа
        /// </summary>
        public string Caption { get; set; }
        /// <summary>
        /// включен ли лист
        /// </summary>
        public bool Enable { get; set; }
        /// <summary>
        ///  Слои этого листа
        /// </summary>
        public StringCollection Layers { get; set; }
        /// <summary>
        /// макет 
        /// </summary>
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
        private Image m_Image;


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
    }

}
