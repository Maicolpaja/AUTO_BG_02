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
    class cmd_ExportElementInformation : IExternalCommand
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
            //TaskDialog.Show("Mensaje de Exito", "El command app Funciona correctamente");

            #region Instrucciones
            ///1. Seleccionar Elementos
            ///2. Trabajar con excel
            ///3. Exportar Informacion de cada elemento a excel
            #endregion


            #region Seleccionar elementos
            IList<Reference> pickedObjs;
            pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Seleccione los elementos");
            List<Element> els = (from Reference r in pickedObjs select doc.GetElement(r.ElementId)).ToList();
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
            //ImageExportOptions op = new ImageExportOptions
            //{
            //    ZoomType = ZoomFitType.FitToPage,
            //    PixelSize = 2024,
            //    FilePath = "D:\\Usuarios\rrios\\Escritorio" + @"\" + "test_image",
            //    FitDirection = FitDirectionType.Horizontal,
            //    HLRandWFViewsFileType = ImageFileType.PNG,
            //    ShadowViewsFileType = ImageFileType.PNG,
            //};

            #endregion

            #region Exportar Informacion de cada elemento a Excel

            double filaInicio = 1;

            foreach (Element el in els)
            {
                ws.Cells[filaInicio, 1] = el.Id.ToString();
                ws.Cells[filaInicio, 2] = ParameterToString(el.LookupParameter("Elemento"));
                //doc.ExportImage();

                filaInicio++;

            }
            excel.Visible = true;
            #endregion
            return Result.Succeeded;
        }
        public string ParameterToString(Parameter param)
        {
            string val = "none";

            if (param == null)
            {
                return val;
            }
            switch (param.StorageType)
            {
                case StorageType.Double:
                    double dval = param.AsDouble();
                    val = dval.ToString();
                    break;
                case StorageType.Integer:
                    int ival = param.AsInteger();
                    val = ival.ToString();
                    break;
                case StorageType.String:
                    string sval = param.AsString();
                    val = sval;
                    break;
                case StorageType.ElementId:
                    ElementId idval = param.AsElementId();
                    val = idval.IntegerValue.ToString();
                    break;
                case StorageType.None:
                    break;
            }

            return val;
        }

    }
}
