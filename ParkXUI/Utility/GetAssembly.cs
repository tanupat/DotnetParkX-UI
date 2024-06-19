using System.Reflection;

namespace ParkXUI.Utility;

public class GetAssembly
{
    public static T GetAssemblyAttribute<T>(Assembly assembly) where T : Attribute
    {
        return (T)Attribute.GetCustomAttribute(assembly, typeof(T));
    }
}