#region Namespaces
using Autodesk.Revit.UI;
#endregion

namespace RemovePaintTool
{
    class App : IExternalApplication

    {
        public Result OnStartup(UIControlledApplication application)
        {
            var UI = new SetupInterface(application, "TabName");
            UI.Initialize("PanelName");

            return Result.Succeeded;
        }
        public Result OnShutdown(UIControlledApplication application)
        {
            return Result.Succeeded;
        }
    }
}
