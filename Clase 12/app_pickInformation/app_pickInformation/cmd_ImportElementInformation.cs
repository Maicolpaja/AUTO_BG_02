using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces 

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.UI.Selection;

using X = Microsoft.Office.Interop.Excel;
using System.Windows.Forms;
using System.Reflection;



namespace app_pickInformation
{
    [Transaction(TransactionMode.Manual)]
    class cmd_ImportElementInformation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, 
                              ref string message, 
                              ElementSet elements)
        {
            #region Declaracion de varables para acceder al documento
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;
            #endregion

            #region Instrucciones
            ///1. Trabajar Excel
            ///2. Buscar elemento por ID
            ///3. Guardar informacion del elemento
            #endregion

            #region Trabajar con Excel

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

            //TaskDialog.Show("FileName: ", dlg.FileName);

            X.Workbook wb = excel.Workbooks.Open(dlg.FileName, Missing.Value, Missing.Value,
                                                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                Missing.Value, Missing.Value, Missing.Value, Missing.Value,
                                                Missing.Value, Missing.Value, Missing.Value, Missing.Value);

            X.Worksheet ws = excel.ActiveWorkbook.Sheets["Element Information"];

            excel.Visible = false;
            #endregion

            //Obtenemos el Elemento
            TaskDialog.Show("ID:" ,Convert.ToInt32(ws.Cells[2, 1].value()));
            Element el = doc.GetElement( Convert.ToInt32(ws.Cells[2, 1].value()));

            using (Transaction t1 = new Transaction(doc, "T01"))
            {
                t1.Start();

                /// Parametro 01 - Donde guardamos la información
                var parameters = el.GetParameters("Parametro Propio");
                if (parameters.Count == 0)
                {
                    TaskDialog.Show("Mensaje de Error", "El parametro seleccionado no existe");
                    return Result.Cancelled;
                }
                var parameter = parameters.First();
                /// Parametro 02 - Donde obtenemos la información
                parameter.Set(ws.Cells[2, 3].value());
                t1.Commit();
            }


            return Result.Succeeded;
        }
    }
}
