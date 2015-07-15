using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SharedClasses
{
    public partial class frmBaseKoord_list : frmBaseSpr_list
    {
        public frmBaseKoord_list()
            : base()
        {
            InitializeComponent();
        }

        public frmBaseKoord_list(bool isSelectMode = false, string filteredString = "")
            : base(isSelectMode, filteredString)
        {
            InitializeComponent();
        }
    }
}
