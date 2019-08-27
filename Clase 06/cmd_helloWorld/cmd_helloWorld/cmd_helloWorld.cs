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



namespace cmd_helloWorld
{
    [Transaction(TransactionMode.ReadOnly)]
    public class cmd_helloWorld : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, 
                              ref string message, 
                              ElementSet elements)
        {
            TaskDialog.Show("Mi primer mensaje", "Este es mi primer Hola Mundo");
            return Result.Succeeded;

        }
    }
}
