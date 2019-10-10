using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces 
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;


namespace app_pickInformation
{
    [Transaction(TransactionMode.Manual)]
    class cmd_setParameters : IExternalCommand
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

            #region Seleccion de elementos
            IList<Reference> pickedObjs;
            pickedObjs = uidoc.Selection.PickObjects(ObjectType.Element, "Seleccione los elementos");
            List<Element> els = (from Reference r in pickedObjs select doc.GetElement(r.ElementId)).ToList();
            #endregion

            //TaskDialog.Show("Cantidad de Elementos", "Usted selecciono " + els.Count.ToString());
            frm_selectParameters frm = new frm_selectParameters();
            frm.ShowDialog();

            if (frm_selectParameters.ejecutar)
            {
                foreach (Element el in els)
                {
                    using (Transaction t1 = new Transaction(doc, "T01"))
                    {
                        t1.Start();

                        /// Parametro 01 - Donde guardamos la información
                        var parameters = el.GetParameters(frm_selectParameters.parametro_01);
                        if (parameters.Count == 0)
                        {
                            TaskDialog.Show("Mensaje de Error", "El parametro seleccionado no existe");
                            return Result.Cancelled;
                        }
                        var parameter = parameters.First();
                        /// Parametro 02 - Donde obtenemos la información
                        parameter.Set(ParameterToString(el.LookupParameter(frm_selectParameters.parametro_02)) + " Automatización");
                        t1.Commit();
                    }
                }
                TaskDialog.Show("Mensaje de Exito", "Se modificaron " + els.Count.ToString() + " elementos");
                return Result.Succeeded;
            }
            else
            {
                TaskDialog.Show("Mensaje de Error", "No se modifico los elementos por que el usuario cancelo la ejecución");
                return Result.Cancelled;
            }


            ///###CASO DONDE SELECCIONE UN ELEMENTO###
            ///1. SELECCIONAR SOLO UN ELEMENTO
            ///uidoc.Selection.PickObject(ObjectType.Element);
            ///2. OBTENDRIA LOS PARAMETROS DEL ELEMENTO el.getorderParameters
            ///3. RECORRERIA TODOS LOS PARAMETROS Y OBTENDRIA SU valueString y lo guardaria dentro de una lista de String.
            ///List<string>
            ///4. LO MOSTRARIA EN UN COMBOBOX
            ///Cmb_any.datasource


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
