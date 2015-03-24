using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESRI.ArcGIS.Geodatabase;
using System.ComponentModel;


namespace WorckWithReestr
{

    //class BindingListView<T> : TableWraper
    //{ }

    class ArcGisListBinding : TableWraper
        //  IBindingListView <IRow>
    {
        public ArcGisListBinding(ITable tableToWrap, string fildsToSorted = null):
            base(tableToWrap, fildsToSorted)
        {
            UseCVDomains = true;
        }


        //// find
        //protected override bool SupportsSearchingCore
        //{
        //    get
        //    {
        //        return true;
        //    }
        //}
        //protected override int FindCore(System.ComponentModel.PropertyDescriptor prop, object key)
        //{
        //    //return base.FindCore(prop, key);
        //    return -1;
        //}




    }
}
