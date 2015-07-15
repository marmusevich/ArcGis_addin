﻿using System.Windows.Forms;
using SharedClasses;

namespace WorckWithKadastr
{
    public partial class frmKoordynatyDorog_list : frmBaseKoord_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------
        
        public static void ShowForView(string filteredString = "")
        {
            Form frm = new frmKoordynatyDorog_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmKoordynatyDorog_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmKoordynatyDorog_list() : base()
        {
            InitializeComponent();
        }
        
        public frmKoordynatyDorog_list(bool isSelectMode = false, string filteredString = "") : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "AdrReestr";
            base.NameTable = "AdrReestr.DBO.KoordynatyDorogy";
            base.NameSortFild = "KodObject";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmKoordynatyDorog_element(_objectID, _editMode);
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
