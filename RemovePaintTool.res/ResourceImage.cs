using System.Reflection;
using System.Windows.Media.Imaging;

namespace RemovePaintTool.res
{
    public static class ResourceImage
    {
        /// <summary>
        /// Return BitmapImage based on provides images
        /// </summary>
        /// <param name="name">Name of image with extension</param>
        /// <returns></returns>
        public static BitmapImage GetImage(string name)
        {
            var assembly = Assembly.GetExecutingAssembly();
            var stream = assembly.GetManifestResourceStream(typeof(ResourceImage).Namespace + ".Images.Icons." + name);

            var image = new BitmapImage();
            image.BeginInit();
            image.StreamSource = stream;
            image.EndInit();

            return image;
        }
    }
}
