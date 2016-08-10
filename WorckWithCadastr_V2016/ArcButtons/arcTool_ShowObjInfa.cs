using System;
using SharedClasses;

namespace WorckWithKadastr2016
{
    public class arcTool_ShowObjInfa : ESRI.ArcGIS.Desktop.AddIns.Tool
    {
        public arcTool_ShowObjInfa()
        {
            AppStartPoint.Init();
        }

        protected override void OnUpdate()
        {
            Enabled = ArcMap.Application != null;
        }

        protected override void OnMouseUp(ESRI.ArcGIS.Desktop.AddIns.Tool.MouseEventArgs arg)
        {
            try
            {


                GeneralMapWork.SelectFeaturesScreenAndPartialRefresh(arg.X, arg.Y);

                string table_name = "";
                int objectID = -1;
                string[] allTableName = { "Ztl_Bdn", 
                                      "Grm_Bdl", 
                                      "Kvt", 
                                      "Obj_Scl_Sft_Ecn", 
                                      "Rej_Adm_Raj_Mis", 
                                      "Rej_Adr_Osnov", 
                                      "Rej_Adr_Poh", 
                                      "Rej_Bud_Adr", 
                                      "Rej_Vul", 
                                      "Vrb_Bdl_Spr"};

                //запросить имя таблицы и ИД выброного - в общем класе
                // там же форма выбора если выбрано больше чем одна запись
                GeneralMapWork.GetSelectedTableNameAndObjectID(out table_name, out objectID, ref allTableName);

                //здесь - процедура сапостовления, и вызов формы на просмотр
                frmBaseElement frm = GetElementForm(table_name, objectID);
                if (frm != null)
                {
                    frm.ShowDialog();
                    frm.Dispose();
                }
                //отключить выделение, отключить текущий инструмент
                GeneralMapWork.ClearMapSelection();
                GeneralMapWork.ClearAllLayerSelection();
                ArcMap.Application.CurrentTool = null;

            }
            catch (Exception ex) // обработка ошибок
            {
                Logger.Write(ex, "Ошибка при показе информации об объекте на карте");
                GeneralApp.ShowErrorMessage("Ошибка при показе информации об объекте на карте");
            }
        }

        //вернуть форму элемента по имени 
        private frmBaseElement GetElementForm(string table_name, int objectID)
        {
            frmBaseElement frm = null;
            if (!(table_name == "" || objectID == -1) && table_name.Contains("Kadastr2016"))
            {
                if (table_name.Contains("Ztl_Bdn"))
                    frm = new frmZtl_Bdn_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Grm_Bdl"))
                    frm = new frmGrm_Bdl_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Kvt"))
                    frm = new frmKvt_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Obj_Scl_Sft_Ecn"))
                    frm = new frmObj_Scl_Sft_Ecn_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Rej_Adm_Raj_Mis"))
                    frm = new frmRej_Adm_Raj_Mis_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Rej_Adr_Osnov"))
                    frm = new frmRej_Adr_Osnov_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Rej_Adr_Poh"))
                    frm = new frmRej_Adr_Poh_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Rej_Bud_Adr"))
                    frm = new frmRej_Bud_Adr_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Rej_Vul"))
                    frm = new frmRej_Vul_element(objectID, frmBaseSpr_element.EditMode.EDIT);
                else if (table_name.Contains("Vrb_Bdl_Spr"))
                    frm = new frmVrb_Bdl_Spr_element(objectID, frmBaseSpr_element.EditMode.EDIT);
            }
            return frm;
        }


    }
}
