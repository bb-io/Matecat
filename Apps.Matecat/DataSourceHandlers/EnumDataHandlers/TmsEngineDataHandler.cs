using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class TmsEngineDataHandler : IStaticDataSourceHandler
{
    public Dictionary<string, string> GetData() => new()
    {
        { "0", "Disabled" },
        { "1", "MyMemory (Default)" }
    };
}