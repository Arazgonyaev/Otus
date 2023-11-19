using Newtonsoft.Json.Linq;

namespace Otus_16_endpoint;

public static class StringExtensions
{
    public static string GetArg(this string jsonStr, string arg)
    {
        dynamic data = JObject.Parse(jsonStr);

        return data[arg].ToString();
    }
}
