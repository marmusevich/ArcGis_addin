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
    public partial class frmBaseAdrReestrSpr_element : frmBaseSpr_element
    {

        public frmBaseAdrReestrSpr_element()
            : base()
        {
            InitializeComponent();
        }
        public frmBaseAdrReestrSpr_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();

        }

    }
}
