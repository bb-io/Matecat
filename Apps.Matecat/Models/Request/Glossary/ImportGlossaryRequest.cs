using Apps.Matecat.DataSourceHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Matecat.Models.Request.Glossary;

public class ImportGlossaryRequest
{
    public FileReference Glossary { get; set; }
    
    [Display("Translation memory key")]
    [DataSource(typeof(PrivateTranslationMemoryDataHandler))] // Glossaries can be added as private resource only
    public string? TranslationMemoryKey { get; set; }
    
    [Display("Glossary name")]
    public string? GlossaryName { get; set; }
}