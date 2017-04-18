using ESRI.ArcGIS.Carto;
using System;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;

namespace CadastralReference
{
    [Serializable]
    public class OneLayerDescriptions : IEquatable<OneLayerDescriptions>
    {
        /// <summary>
        /// тип слоя
        /// </summary>
        public enum LayerType : short
        { 
            Raster = 1,
            Feature = 2,
        }


        public OneLayerDescriptions()
        {
            Type = LayerType.Feature;
            DataPath = "";
        }


        public OneLayerDescriptions(ILayer layer)
        {
            Type = LayerType.Feature;
            DataPath = "";
        }


        //скопировать настройки
        public void CopySetingFrom(OneLayerDescriptions old)
        {
            this.Type = old.Type;
            this.DataPath = old.DataPath;
        }



        #region свойсва / поля
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("LayerType")]
        public LayerType Type { get; set; }
        /// <summary>
        /// 
        /// </summary>
        [XmlElement("DataPath")]
        public string DataPath { get; set; }


        //  родитель - ?


        #endregion

        #region перегрузка стандартных функций
        public override string ToString()
        {
            return "Caption";
        }

        public override bool Equals(object obj)
        {
            if (obj == null) return false;
            OneLayerDescriptions objAsOLD = obj as OneLayerDescriptions;
            if (objAsOLD == null) return false;
            else return Equals(objAsOLD);
        }

        public bool Equals(OneLayerDescriptions other)
        {
            if (other == null) return false;
            return ( this.Type.Equals(other.Type) && this.DataPath.Equals(other.DataPath));
        }

        public override int GetHashCode()
        {
            return DataPath.GetHashCode() * ((short)this.Type);
        }
        #endregion
    }
}
