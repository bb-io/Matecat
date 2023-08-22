using Apps.Matecat.DataSourceHandlers;
using Apps.Matecat.DataSourceHandlers.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.Project;

public class CreateProjectRequest
{
    [JsonProperty("project_name")]
    [Display("Project name")]
    public string ProjectName { get; set; }

    [JsonProperty("source_lang")]
    [Display("Source language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string SourceLanguage { get; set; }

    [JsonProperty("target_lang")]
    [Display("Target language")]
    [DataSource(typeof(LanguageDataHandler))]
    public string TargetLanguage { get; set; }

    [JsonProperty("tms_engine")]
    [Display("TMS engine")]
    [DataSource(typeof(TmsEngineDataHandler))]
    public string? TmsEngine { get; set; }

    [JsonProperty("mt_engine")]
    [Display("MT engine")]
    [DataSource(typeof(MtEngineDataHandler))]
    public int? MtEngine { get; set; }

    [JsonProperty("private_tm_key")]
    [Display("Private TM key")]
    public string? TmKey { get; set; }

    [JsonProperty("subject")]
    public string? Subject { get; set; }

    [JsonProperty("segmentation_rule")]
    [Display("Segmentation rule")]
    public string? SegmentationRule { get; set; }

    [JsonProperty("owner_email")]
    [Display("Owner email")]
    public string? OwnerEmail { get; set; }

    [JsonProperty("id_team")]
    [Display("Id team")]
    [DataSource(typeof(TeamDataHandler))]
    public string? IdTeam { get; set; }

    [JsonProperty("lexiqa")]
    public string? LexiQa { get; set; }

    [Display("Speech-to-text")] public int? Speech2Text { get; set; }

    [JsonProperty("get_public_matches")]
    [Display("Get public matches")]
    public bool? GetPublicMatches { get; set; }

    [JsonProperty("pretranslate_100")]
    [Display("Pre-translate 100% matches from TM")]
    public int? Pretranslate100 { get; set; }

    [JsonProperty("metadata")]
    public string? Metadata { get; set; }
}