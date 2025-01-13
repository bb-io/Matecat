using Apps.Matecat.Constants;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class DerivedStatusDataHandler : IStaticDataSourceItemHandler
{
    private static Dictionary<string, string> Data => new()
    {
        {JobStatus.New, "New"},
        {JobStatus.InTranslation, "In translation"},
        {JobStatus.Translated, "Translated"},
        {JobStatus.InRevision, "In revision"},
        {JobStatus.Revised, "Revised"},
    };

    public IEnumerable<DataSourceItem> GetData()
    {
        return Data.Select(x => new DataSourceItem(x.Key, x.Value));
    }
}