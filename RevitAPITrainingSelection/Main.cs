using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Mechanical;
using Autodesk.Revit.DB.Plumbing;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RevitAPITrainingSelection
{
    [Transaction(TransactionMode.Manual)]
    public class Main : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Document doc = uidoc.Document;


            var levels = new FilteredElementCollector(doc)
                .OfClass(typeof(Level))
                .OfType<Level>()
                .ToList();

            string Text = null;
            foreach (Level level in levels)
            {
                var ducts = new FilteredElementCollector(doc)
                    .OfClass(typeof(Duct))
                    .OfType<Duct>()
                    .Where ( x=> x.get_Parameter(BuiltInParameter.RBS_START_LEVEL_PARAM).AsValueString()==level.Name)
                    .Count();
                Text += level.Name + ": " + ducts.ToString() + "воздуховодов" + "\n";
            }
            TaskDialog.Show("Количество воздуховодов по этажам", Text);

            return Result.Succeeded;
        }
    }
}
