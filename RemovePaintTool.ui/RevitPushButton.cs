using Autodesk.Revit.UI;
using System;
using RemovePaintTool.res;

namespace RemovePaintTool.ui
{
    public static class RevitPushButton
    {
        /// <summary>
        /// Create new PushButton based on provide data.
        /// </summary>
        /// <param name="data">Name of RevitPushButtonDataModel</param>
        /// <returns>PushButton</returns>
        public static PushButton Create(RevitPushButtonDataModel data)
        {
            var guid = Guid.NewGuid().ToString();
            var pushButtonDataModel = new PushButtonData(guid, data.Label, data.AssemblyLocation, data.ClassName)
            {
                Image = ResourceImage.GetImage(data.ImageName),
                ToolTipImage = ResourceImage.GetImage(data.ToolTipImageName),
                LargeImage = ResourceImage.GetImage(data.LargeImageName)
            };

            return (PushButton)data.ribbonPanel.AddItem(pushButtonDataModel);
        }
    }
}
