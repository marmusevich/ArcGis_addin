﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CadastralReference

{
    public partial class frmHelpTemplateView : Form
    {
        public frmHelpTemplateView()
        {
            InitializeComponent();
        }

        private void frmHelpTemplateView_Load(object sender, EventArgs e)
        {
            this.lblOpisanie.Text = TextTemplateConverter.GetDiscription();
        }
    }
}