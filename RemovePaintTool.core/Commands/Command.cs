#region Namespaces
using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
#endregion

namespace RemovePaintTool.core
{
    [Transaction(TransactionMode.Manual)]
    public class Command : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            UIApplication uiapp = commandData.Application;
            UIDocument uidoc = uiapp.ActiveUIDocument;
            Application app = uiapp.Application;
            Document doc = uidoc.Document;

            List<string> filteredElements = new List<string> { "Walls", "Floors", "Ceilings" };

            List<Reference> newrefs = new ElementReferance(doc).GetReferences();

            TaskDialog.Show("Color Wall Reset", $"In active view there are {newrefs.Count} painted elements", TaskDialogCommonButtons.Ok);
            IList<Reference> refIDS;

            try
            {
                refIDS = uidoc.Selection.PickObjects(ObjectType.Element,
                new ElementSelFilter(filteredElements),
                "ADD/REMOVE elements",
                newrefs);
            }
            catch (Exception)
            {
                return Result.Cancelled;
            }

            foreach (var item in refIDS)
            {
                Element e = doc.GetElement(item);
                ElementChange elementChange = new ElementChange(doc);

                if (elementChange.IsElementPainted(e))
                    while (elementChange.IsFacePainted(elementChange.geometryElement, e.Id))
                    {
                        using (var transaction = new Transaction(doc, "Remove paint"))
                        {
                            transaction.Start();
                            doc.RemovePaint(elementChange.Id, elementChange.Face);
                            transaction.Commit();
                        }
                    }
            }

            return Result.Succeeded;
        }
        public static string GetPath()
        {
            return typeof(Command).Namespace + "." + nameof(Command);
        }
    }
}