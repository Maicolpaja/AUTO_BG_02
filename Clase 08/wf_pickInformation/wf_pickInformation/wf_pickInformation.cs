using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//Namespaces 
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using Autodesk.Revit.Attributes;


namespace wf_pickInformation
{
    [Transaction(TransactionMode.Manual)]
    public class wf_pickInformation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
                              ref string message,
                              ElementSet elements)
        {
            #region DECLARACION DE VARIABLES PARA OBTNER EL DOCUMENTO
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;
            #endregion

            #region SELECCION DE ELEMENTOS
            IList<Reference> pickedObjs;
            pickedObjs = uiDoc.Selection.PickObjects(ObjectType.Element, "Seleccione los elementos");
            List<Element> els = (from Reference r in pickedObjs select doc.GetElement(r.ElementId)).ToList();
            #endregion
            //TaskDialog.Show("Mensaje de prueba", "Usted ha seleccionado: " + els.Count.ToString());
            List<ElementData> data = new List<ElementData>();

            foreach (Element el in els)
            {
                data.Add(new ElementData(
                    el.Id.ToString(),
                    ParameterToString(el.LookupParameter("Elemento")),
                    ParameterToString(el.LookupParameter("Nivel del Elemento")),
                    ParameterToString(el.LookupParameter("Volume"))
                    ));
            }
            (new frm_PickInformation(data)).Show();
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
