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

            foreach (Element el in els)
            {
                using(Transaction t1 = new Transaction(doc, "T01"))
                {
                    t1.Start();

                    var parameters = el.GetParameters("Parametro Propio");
                    var parameter = parameters.First();
                    parameter.Set(ParameterToString(el.LookupParameter("Elemento")) + " Modificado");
                    t1.Commit();
                }
            }
            TaskDialog.Show("Mensaje de Exito", "Se modificaron " + els.Count.ToString() + " elementos");
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
