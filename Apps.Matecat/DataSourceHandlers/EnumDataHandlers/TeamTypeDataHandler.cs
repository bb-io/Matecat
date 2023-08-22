using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class TeamTypeDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"general", "General"}
    };
}