using Apps.Matecat.DataSourceHandlers;
using Apps.Matecat.DataSourceHandlers.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Dynamic;

namespace Apps.Matecat.Models.Request.Project;

public class CreateProjectRequest
{
    [Display("Project name")]
    public string ProjectName { get; set; }

    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [Display("Target languages")]
    [DataSource(typeof(LanguageDataHandler))]
    public IEnumerable<string> TargetLanguages { get; set; }

    [Display("TMS engine")]
    [StaticDataSource(typeof(TmsEngineDataHandler))]
    public string? TmsEngine { get; set; }

    [Display("MT engine")]
    [DataSource(typeof(MtEngineDataHandler))]
    public string? MtEngine { get; set; }

    [Display("Private TM keys")]
    [DataSource(typeof(TranslationMemoryDataHandler))]
    public IEnumerable<string>? TmKey { get; set; }

    [Display("Subject")]
    [StaticDataSource(typeof(SubjectDataHandler))]
    public string? Subject { get; set; }

    [Display("Segmentation rule")]
    [StaticDataSource(typeof(SegmentationRuleDataHandler))]
    public string? SegmentationRule { get; set; }

    [Display("Due date")]
    public DateTime? DueDate { get; set; }

    [Display("Team")]
    [DataSource(typeof(TeamDataHandler))]
    public string? IdTeam { get; set; }

    [Display("Enable LexiQa?")]
    public bool? LexiQa { get; set; }

    [Display("Speech-to-text")] 
    public bool? Speech2Text { get; set; }

    [Display("Get public matches")]
    public bool? GetPublicMatches { get; set; }

    [Display("Pre-translate 100% matches from TM")]
    public bool? Pretranslate100 { get; set; }

    [Display("Metadata")]
    public string? Metadata { get; set; }
}