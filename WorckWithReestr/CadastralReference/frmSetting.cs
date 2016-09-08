﻿using System;
using System.Windows.Forms;

namespace CadastralReference
{
    public partial class frmSetting : Form
    {


        public frmSetting()
        {
            InitializeComponent();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            // продвинутое сохранение


            for (int i = 0; i < clbListOfPages.Items.Count; i++)
            {
                OnePageDescriptions opd = clbListOfPages.Items[i] as OnePageDescriptions;
                // проверки
                WorkCadastralReference.GetCadastralReferenceData().Pages[i] = opd;
            }




            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        private void frmSetting_Load(object sender, EventArgs e)
        {

            CreateTabsTotcPages();
            FillclbListOfPages();
        }



        private void FillclbListOfPages()
        {
            foreach (OnePageDescriptions pd in WorkCadastralReference.GetCadastralReferenceData().Pages)
            {
                clbListOfPages.Items.Add(pd, pd.Enable);
            }
        }


        private void CreateTabsTotcPages()
        {
            foreach (OnePageDescriptions pd in WorkCadastralReference.GetCadastralReferenceData().Pages)
            {


                TabPage tp = new TabPage();
                tp.Location = new System.Drawing.Point(4, 22);
                tp.Name = pd.PagesID.ToString();
                tp.Padding = new System.Windows.Forms.Padding(3);
                tp.Size = new System.Drawing.Size(637, 389);
                //tp.TabIndex = 0;
                tp.Text = pd.Caption;
                tp.UseVisualStyleBackColor = true;


                this.tcPages.Controls.Add(tp);


                //остальные элементы управления
            }

        }

        private void clbListOfPages_ItemCheck(object sender, ItemCheckEventArgs e)
        {


            OnePageDescriptions opd = clbListOfPages.Items[e.Index] as OnePageDescriptions;
            opd.Enable = e.NewValue == CheckState.Checked;


        }




    }
}
