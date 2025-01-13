using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers;

public class SubjectDataHandler : IStaticDataSourceItemHandler
{
    private Dictionary<string, string> Data => new()
    {
        {"general", "General"},
        {"accounting_finance", "Accounting & Finance"},
        {"adwords", "Adwords"},
        {"aerospace_defence", "Aerospace / Defence"},
        {"architecture", "Architecture"},
        {"art", "Art"},
        {"automotive", "Automotive"},
        {"certificates_diplomas_licences_cv_etc", "Certificates, diplomas, licences , cv's, etc"},
        {"chemical", "Chemical"},
        {"civil_engineering_construction", "Civil Engineering / Construction"},
        {"corporate_social_responsibility", "Corporate Social Responsibility"},
        {"cosmetics", "Cosmetics"},
        {"culinary", "Culinary"},
        {"electronics_electrical_engineering", "Electronics / Electrical Engineering"},
        {"energy_power_generation_oil_gas", "Energy / Power generation / Oil & Gas"},
        {"environment", "Environment"},
        {"european_governmental_politics", "European Governmental Politics"},
        {"fashion", "Fashion"},
        {"games_viseogames_casino", "Games / Video Games / Casino"},
        {"general_business_commerce", "General Business / Commerce"},
        {"history_archaeology", "History / Archaeology"},
        {"information_technology", "Information Technology"},
        {"insurance", "Insurance"},
        {"internet_e-commerce", "Internet, e-commerce"},
        {"legal_documents_contracts", "Legal documents / Contracts"},
        {"literary_translations", "Literary Translations"},
        {"marketing_advertising_material_public_relations", "Marketing & Advertising material / Public Relations"},
        {"matematics_and_physics", "Mathematics and Physics"},
        {"mechanical_manufacturing", "Mechanical / Manufacturing"},
        {"media_journalism_publishing", "Media / Journalism / Publishing"},
        {"medical_pharmaceutical", "Medical / Pharmaceutical"},
        {"music", "Music"},
        {"private_correspondence_letters", "Private Correspondence, Letters"},
        {"religion", "Religion"},
        {"science", "Science"},
        {"shipping_sailing_maritime", "Shipping / Sailing / Maritime"},
        {"social_science", "Social Science"},
        {"telecommunications", "Telecommunications"},
        {"travel_tourism", "Travel & Tourism"},
    };
    
    public IEnumerable<DataSourceItem> GetData()
    {
        return Data.Select(x => new DataSourceItem(x.Key, x.Value));
    }
}