using System.Reflection;

namespace RemovePaintTool.core
{
    public static class AssemblyCore
    {
        public static string GetAssemblyLocation()
        {
            return Assembly.GetExecutingAssembly().Location;
        }
    }
}
