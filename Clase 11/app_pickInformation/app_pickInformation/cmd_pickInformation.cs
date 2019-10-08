using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//NameSpaces 

using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.Attributes;

namespace app_pickInformation
{
    [Transaction(TransactionMode.ReadOnly)]
    public class cmd_pickInformation : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData,
                              ref string message,
                              ElementSet elements)
        {
            TaskDialog.Show("Titulo de Mensaje", "Este es un mensaje de prueba");
            return Result.Succeeded;
        }
    }
}
