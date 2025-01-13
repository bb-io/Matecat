using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class TimeZoneDataSource : IStaticDataSourceItemHandler
{
    private static Dictionary<string, string> Data => new()
    {
        { "0", "UTC" },
        { "1", "UTC+1" },
        { "2", "UTC+2" },
        { "3", "UTC+3" },
        { "4", "UTC+4" },
        { "5", "UTC+5" },
        { "6", "UTC+6" },
        { "7", "UTC+7" },
        { "8", "UTC+8" },
        { "9", "UTC+9" },
        { "10", "UTC+10" },
        { "11", "UTC+11" },
        { "12", "UTC+12" },
        { "-1", "UTC-1" },
        { "-2", "UTC-2" },
        { "-3", "UTC-3" },
        { "-4", "UTC-4" },
        { "-5", "UTC-5" },
        { "-6", "UTC-6" },
        { "-7", "UTC-7" },
        { "-8", "UTC-8" },
        { "-9", "UTC-9" },
        { "-10", "UTC-10" },
        { "-11", "UTC-11" },
        { "-12", "UTC-12" }
    };

    public IEnumerable<DataSourceItem> GetData()
    {
        return Data.Select(x => new DataSourceItem(x.Key, x.Value));
    }
}