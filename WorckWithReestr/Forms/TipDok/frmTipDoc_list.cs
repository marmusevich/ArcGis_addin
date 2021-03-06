﻿using System.Windows.Forms;
using SharedClasses;

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

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Tip_Doc";
            base.NameSortFild = "Kod_Doc";
        }

        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            dgv.Columns["GlobalID"].Visible = false;
        }


        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmTipDoc_element(_objectID, _editMode);
        }
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            dictionaryTableName = "";
            return false;
        }

        #endregion
    }


}
