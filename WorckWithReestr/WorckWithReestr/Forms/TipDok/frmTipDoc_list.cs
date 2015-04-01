using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WorckWithReestr
{
    public partial class frmTipDoc_list : frmBaseSpr_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmTipDoc_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmTipDoc_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmTipDoc_list()
            : base()
        {
            InitializeComponent();
        }

        public frmTipDoc_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.Tip_Doc";
            base.NameSortFild = "Kod_Doc";
        }


        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmTipDoc_element(_objectID, _editMode);
        }

        #endregion
    }


}
