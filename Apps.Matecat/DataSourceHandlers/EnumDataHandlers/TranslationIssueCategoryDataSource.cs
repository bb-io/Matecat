using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class TranslationIssueCategoryDataSource : IStaticDataSourceItemHandler
{
    private static Dictionary<string, string> Data => new()
    {
        {"27207467", "Style (readability, consistent style and tone)"},
        {"27207468", "Tag issues (mismatches, whitespaces)"},
        {"27207469", "Translation errors (mistranslation, additions or omissions)"},
        {"27207470", "Terminology and translation consistency"},
        {"27207471", "Language quality (grammar, punctuation, spelling)"},
    };
    
    public IEnumerable<DataSourceItem> GetData()
    {
        return Data.Select(x => new DataSourceItem(x.Key, x.Value));
    }
}