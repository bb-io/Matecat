using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class MtEngineDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"0", "Disabled"},
        {"1", "Get MT from MyMemory"}
    };
}