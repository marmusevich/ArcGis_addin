using ESRI.ArcGIS.Carto;
using ESRI.ArcGIS.Geodatabase;
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
            Other = 0,
            Group = 1,
            Raster = 2,
            Feature = 3,
        }

        public OneLayerDescriptions()
        {
            Type = LayerType.Other;
            DataPath = "";
            Caption = "";
        }

        public OneLayerDescriptions(OneLayerDescriptions old)
        {
            CopySetingFrom(old); 
        }

        public OneLayerDescriptions(ILayer layer)
        {
            Caption = layer.Name;
            if (layer is ICompositeLayer)
            {
                Type = LayerType.Group;
                DataPath = layer.Name;
            }
            else if (layer is IFeatureLayer2)
            {
                Type = LayerType.Feature;
                IFeatureLayer2 selectedFL = layer as IFeatureLayer2;
                if (selectedFL.FeatureClass is IDataset)
                {
                    IDataset dataset = (IDataset)(selectedFL.FeatureClass);
                    DataPath = dataset.Name;
                }
                else
                {
                    DataPath = layer.Name;
                }
            }
            else if (layer is IRasterLayer)
            {
                IRasterLayer selectedRL = layer as IRasterLayer;
                Type = LayerType.Feature;
                DataPath = selectedRL.FilePath;
            }
            else
            {
                Type = LayerType.Other;
                DataPath = layer.Name;
            }
        }

        //скопировать настройки
        public void CopySetingFrom(OneLayerDescriptions old)
        {
            this.Type = old.Type;
            this.DataPath = old.DataPath;
            this.Caption = old.Caption;
        }



        #region свойсва / поля
        /// <summary>
        /// название слоя
        /// </summary>
        [XmlElement("Caption")]
        public string Caption { get; set; }
        /// <summary>
        /// тип слоя
        /// </summary>
        [XmlElement("LayerType")]
        public LayerType Type { get; set; }
        /// <summary>
        /// путь к данным
        /// </summary>
        [XmlElement("DataPath")]
        public string DataPath { get; set; }
        #endregion

        #region перегрузка стандартных функций
        public override string ToString()
        {
            return Caption;
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
