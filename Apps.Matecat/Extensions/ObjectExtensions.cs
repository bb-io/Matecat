using Apps.Matecat.Utils.Converter;
using Newtonsoft.Json;

namespace Apps.Matecat.Extensions;

public static class ObjectExtensions
{
    public static Dictionary<string, string> AsDictionary(this object obj)
    {
        var json = JsonConvert.SerializeObject(obj, new JsonSerializerSettings()
        {
            Converters = {new StringValueConverter()}
        });
        return JsonConvert.DeserializeObject<Dictionary<string, string>>(json)!;
    }
}