﻿using SharedClasses;
using System.Windows.Forms;

namespace WorckWithKadastr2016
{
    public partial class frmObj_Scl_Sft_Ecn_list : frmBase_list
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region functions
        //---------------------------------------------------------------------------------------------------------------------------------------------

        public static void ShowForView(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmObj_Scl_Sft_Ecn_list(false, filteredString);
            frm.Show();
            frm.Activate();
        }

        public static int ShowForSelect(string filteredString = "")
        {
            frmBaseSpr_list frm = new frmObj_Scl_Sft_Ecn_list(true, filteredString);
            frm.ShowDialog();
            return frm.SelectID;
        }

        public frmObj_Scl_Sft_Ecn_list()
            : base()
        {
            InitializeComponent();
        }

        public frmObj_Scl_Sft_Ecn_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();

            base.NameWorkspace = "Kadastr2016";
            base.NameTable = "Obj_Scl_Sft_Ecn";
            base.NameSortFild = "ID_MSB_OBJ";
        }
        protected override frmBaseSpr_element GetElementForm(int _objectID, frmBaseSpr_element.EditMode _editMode)
        {
            return new frmObj_Scl_Sft_Ecn_element(_objectID, _editMode);
        }

        protected override void SetDefaultDisplayOrder()
        {
            int[] displayIndicies = {0,// base.table.FindField("OBJECTID "),// 0
                        base.table.FindField("ID_MSB_OBJ"),
                        base.table.FindField("KOD_TYP_OBJ"),
                        base.table.FindField("N_Kad"),
                        base.table.FindField("Pidcode"),

                        base.table.FindField("Prymitka"),
                        base.table.FindField("KOD_KLS"),
                        base.table.FindField("KOD_STS"),
                        base.table.FindField("RuleID"),

                        base.table.FindField("Override"),
                        base.table.FindField("SHAPE")
                       };
            GeneralApp.SetDisplayOrderByArray(ref dgv, displayIndicies);
        }
        //доп настройка грида
        protected override void OtherSetupDGV()
        {
            dgv.Columns["SHAPE"].Visible = false;
            dgv.Columns["Override"].Visible = false;

            dgv.CellFormatting += OnCellFormatting;
        }
        //вернуть строку доаолнительных условий
        protected override string GetStringAddetConditions()
        {
            string ret = base.GetStringAddetConditions();
            return ret;
        }
        //проверить поле на принадлежность к справочнику, вернуть имя таблици справочника
        public override bool ChekFildIsDictionary(string fildName, ref string dictionaryTableName)
        {
            return false;
        }

        private void OnCellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
        }

        #endregion
    }
}
