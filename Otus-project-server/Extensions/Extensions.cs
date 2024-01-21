using System.Reflection.Metadata.Ecma335;
using Newtonsoft.Json.Linq;

namespace Otus_project_server;

public static class Extensions
{
    public static string GetArg(this string jsonStr, string arg)
    {
        dynamic data = JObject.Parse(jsonStr);

        return data[arg].ToString();
    }

    public static int[] Plus(this int[] a, int[] b) =>
            a.Zip(b, (x, y) => x + y).ToArray();

    public static bool EqualTo<T>(this T[] a, T[] b)
    {
        if (a.Length != b.Length) return false;

        for (int i = 0; i < a.Length; i++)
        {
            if (!a[i].Equals(b[i])) return false;
        }

        return true;
    }
}
