using Autodesk.Revit.UI;

namespace RemovePaintTool.ui
{
    public class RevitPushButtonDataModel
    {
        /// <summary>
        /// Label of PushButton
        /// </summary>
        public string Label { get; set; }
        /// <summary>
        /// RibbonPanel where PushButton should be created
        /// </summary>
        public RibbonPanel ribbonPanel { get; set; }
        public string ToolTip { get; set; }
        public string ToolTipImageName { get; set; }
        public string ImageName { get; set; }
        /// <summary>
        /// Name of main RibbonPanel iamge 
        /// </summary>
        public string LargeImageName { get; set; }
        /// <summary>
        /// Location of command's assembly
        /// </summary>
        public string AssemblyLocation { get; set; }
        /// <summary>
        /// Path to a command (namespace.classname)
        /// </summary>
        public string ClassName { get; set; }

    }
}
