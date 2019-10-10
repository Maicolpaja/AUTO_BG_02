using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces 

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

using X = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;

namespace app_pickInformation
{
    [Transaction(TransactionMode.Manual)]
    class cmd_ExportExcel : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            #region TRABAJAR CON EXCEL

            frm_selectParameters frm = new frm_selectParameters();
            frm.ShowDialog();

            if (frm_selectParameters.ejecutar)
            {
                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Title = "Seleccione un excel";
                dlg.Filter = "Archivos Excel (*.xls;*.xlsx;*.xlsm)|*.xls;*.xlsx;*.xlsm|All files(*)|*";
                if (DialogResult.OK != dlg.ShowDialog())
                {
                    TaskDialog.Show("Mesaje de Error", "Usted no selecciono ningun archivo");
                    return Result.Cancelled;
                }

                X.Application excel = new X.Application();

                if (excel == null)
                {
                    TaskDialog.Show("Mesaje de Error", "Error para crear el archivo excel");
                    return Result.Cancelled;
                }

                X.Workbook wb = excel.Workbooks.Open(dlg.FileName, Missing.Value, Missing.Value,
                                                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                Missing.Value, Missing.Value, Missing.Value, Missing.Value);

                X.Worksheet ws = excel.ActiveWorkbook.Sheets["Hoja1"];

                excel.Visible = false;

               


                ws.Cells[2, 2] = frm_selectParameters.parametro_01;
                ws.Cells[2, 3] = frm_selectParameters.parametro_02;

                TaskDialog.Show("Mensaje desde Excel", ws.Cells[2, 4].value());
                excel.Visible = true;
                //wb.Save();

                #endregion
                return Result.Succeeded;
            }
            else
            {
                return Result.Cancelled;
            }

        }
    }
}
