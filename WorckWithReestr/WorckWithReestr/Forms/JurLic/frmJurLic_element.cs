using ESRI.ArcGIS.Geodatabase;
using System;

namespace WorckWithReestr
{
    public partial class frmJurLic_element : frmBaseSpr_element
    {
        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  types
        //---------------------------------------------------------------------------------------------------------------------------------------------



        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  variables
        //---------------------------------------------------------------------------------------------------------------------------------------------
        DomeinDataAdapter dda;
        
        #endregion


        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  functions
        //---------------------------------------------------------------------------------------------------------------------------------------------


        protected override void DB_to_FormElement(IRow row)
        {
            // взять из базы
            txtID.Text = "" + row.get_Value(0) as string;
            txtName.Text = row.get_Value(1) as string;
            txtOKPO.Text = row.get_Value(2) as string;



            //cbFV - взять из домена поле №3

            //object[] arr = SharedClass.GetListOfDamaibValue(row.Fields.get_Field(3).Domain);
            //if (arr == null)
            //    return;

            ////Array.BinarySearch
            //cbFV.Items.AddRange(arr);


            //ICodedValueDomain codedValueDomain = (ICodedValueDomain)row.Fields.get_Field(3).Domain;
            //System.Collections.Generic.List<SelectData> ret = new System.Collections.Generic.List<SelectData>();
            //for (int i = 0; i < codedValueDomain.CodeCount; i++)
            //{
            //    cbFV.Items.Add(new SelectData(codedValueDomain.get_Value(i), codedValueDomain.get_Name(i)));


            //    if ((row.get_Value(3) != null) && row.get_Value(3).Equals(codedValueDomain.get_Value(i)))
            //        cbFV.SelectedIndex = i;
            //}
            

            object o = row.get_Value(3);
            if(o == null )
                o = row.Fields.get_Field(3).DefaultValue;

            cbFV.SelectedIndex = dda.GetIndexByValue(o);
        }

        protected override void FormElement_to_DB(IRow row)
        {
            // положить в базы
            row.set_Value(1, txtName.Text);
            row.set_Value(2, txtOKPO.Text); // в базе число наверное, а здесь строка
            row.set_Value(3, ((DomeinData)cbFV.SelectedItem).Value);
        }
        #endregion

        //---------------------------------------------------------------------------------------------------------------------------------------------
        #region  form events
        //---------------------------------------------------------------------------------------------------------------------------------------------
        public frmJurLic_element() : base()
        {
            InitializeComponent();
        }

        public frmJurLic_element(int _objectID, EditMode _editMode)
            : base(_objectID, _editMode)
        {
            InitializeComponent();
            base.NameWorkspace = "reestr";
            base.NameTable = "reestr.DBO.jur_osoby";

        }

        private void frmJurLic_element_Load(object sender, EventArgs e)
        {
            switch (editMode)
            {
                case EditMode.ADD:
                    Text = "Добавление нового юридического лица";
                    break;
                case EditMode.EDIT:
                    Text = "Корректировка данных юридического лица";
                    break;
                case EditMode.DELETE:
                    Text = "Удаление юридического лица";
                    btnOk.Text = "Удалить";
                    txtID.Enabled = false;
                    txtName.Enabled = false;
                    txtOKPO.Enabled = false;
                    cbFV.Enabled = false;

                    break;

                default:
                    this.Close();
                    return;
            }

            try
            {
                IFeatureWorkspace fws = SharedClass.GetWorkspace(NameWorkspace) as IFeatureWorkspace;
                ITable table = fws.OpenTable(NameTable);

                //IRow row = table_fizLic.GetRow(objectID);

                dda = new DomeinDataAdapter(table.Fields.get_Field(3).Domain);
                cbFV.Items.AddRange(dda.ToArray());
                object o = table.Fields.get_Field(3).DefaultValue;
                cbFV.SelectedIndex = dda.GetIndexByValue(o);
            }
            catch (Exception ee) // доработать блок ошибок на разные исключения
            {
                SharedClass.ShowErrorMessage();
                this.Close();
            }


            if (editMode != EditMode.ADD)
            {
                if (!this.ReadData()) // error
                {
                    SharedClass.ShowErrorMessage();
                    this.Close();
                }
            }
        }
        #endregion
    }
}
