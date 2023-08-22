using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class SeverityDataHandler : EnumDataHandler
{
    protected override Dictionary<string, string> EnumValues => new()
    {
        {"Neutral", "Neutral"},
        {"Minor", "Minor"},
        {"Major", "Major"},
    };
}