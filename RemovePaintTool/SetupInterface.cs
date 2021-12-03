using Autodesk.Revit.UI;
using RemovePaintTool.core;
using RemovePaintTool.ui;

namespace RemovePaintTool
{
    public class SetupInterface
    {
        private UIControlledApplication _application;
        private string _tabName;

        public SetupInterface(UIControlledApplication application, string tabName)
        {
            _application = application;
            _tabName = tabName;
        }

        public void Initialize(string panelTab)
        {
            _application.CreateRibbonTab(_tabName);
            RibbonPanel ribPanel = _application.CreateRibbonPanel(_tabName, panelTab);

            var revitPushButtonDataModel = new RevitPushButtonDataModel()
            {
                Label = "Tag Wall",
                ToolTip = "Some Tool Tip",
                ImageName = "icon3.jpg",
                LargeImageName = "icon1.png",
                ToolTipImageName = "icon2.png",
                ribbonPanel = ribPanel,
                AssemblyLocation = AssemblyCore.GetAssemblyLocation(),
                ClassName = Command.GetPath()
            };

            var pushButton = RevitPushButton.Create(revitPushButtonDataModel);
        }
    }
}
