using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace totalArea
{
    [Transaction(TransactionMode.ReadOnly)]
    public class totalArea : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, 
                              ref string message, 
                              ElementSet elements)
        {
            UIApplication uiApp = commandData.Application;
            UIDocument uiDoc = uiApp.ActiveUIDocument;
            Document doc = uiDoc.Document;

            //TaskDialog.Show("Titulo del App", "Nombre: " + doc.ProjectInformation.Name);


            //OBTENER AREA DE MUROS
            // 1. SELECCIONAR LOS ELEMENTOS
            IList<Reference> pickedObjs;
            pickedObjs = uiDoc.Selection.PickObjects(Autodesk.Revit.UI.Selection.ObjectType.Element);
            List<ElementId> ids = (from Reference r in pickedObjs select r.ElementId).ToList();

            TaskDialog.Show("Titulo de Apps", "Cantidad de Elementos seleccionados: " + ids.Count.ToString() + " Elementos");
            // 2. SOLO SELECCIONAR LOS ELEMENTOS QUE SEAN WALLS
            List<Element> walls = new List<Element>();
            List<Element> floors = new List<Element>();

            foreach (ElementId id in ids)
            {
                Element el = doc.GetElement(id);
                if(el.Category.Name == "Walls" )
                {
                    walls.Add(el);
                }
                else if (el.Category.Name == "Floors" ){
                    floors.Add(el);

                }

            }
            TaskDialog.Show("Titulo de Apps", "Cantidad de Walls seleccionados: " + walls.Count.ToString() + " Walls" +
                                               "\n Cantidad de Floors Seleccionadas " + floors.Count.ToString()+ " Floors");
            // 3. OBTENER INFORMACION DE LOS ELEMENTOS SELECCIONADOS
            // 4. SUMAR LAS AREAS OBTENIDAS
            double area_losas = 0;
            double area_muros = 0;
            double area_total = 0;
            foreach(ElementId id in ids)
            {
                Element el = doc.GetElement(id);
                if (el.Category.Name == "Walls")
                {
                    area_muros = area_muros + el.LookupParameter("Area").AsDouble() * 0.092903;
                    TaskDialog.Show("Muros", area_losas.ToString());
                }
                else if (el.Category.Name == "Floors")
                {
                    area_losas = area_losas + el.LookupParameter("Area").AsDouble() * 0.092903;
                    TaskDialog.Show("Losas", area_losas.ToString());

                }
                area_total = area_total + el.LookupParameter("Area").AsDouble() * 0.092903;
               //TaskDialog.Show("Area del muro", (e.LookupParameter("Area").AsDouble() * 0.092903).ToString());
            }
            // 5. MOSTRAR AL USUARIO EL RESULTADO
            TaskDialog.Show("Area Total", "Area Total de Muros = " + Math.Round(area_muros, 2).ToString() + "m2"
                            + "\n Area Total de Losas = " + Math.Round(area_losas, 2).ToString() + "m2" +
                            "\n Area Total = " + Math.Round(area_total, 2).ToString() + "m2");


            return Result.Succeeded;
        }
    }
}
