using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.Project;

public class CreateProjectRequest
{
    [JsonProperty("project_name")]
    [Display("Project name")]
    public string ProjectName { get; set; }

    [JsonProperty("source_lang")]
    [Display("Source language")]
    public string SourceLanguage { get; set; }

    [JsonProperty("target_lang")]
    [Display("Target language")]
    public string TargetLanguage { get; set; }

    [JsonProperty("tms_engine")]
    [Display("TMS engine")]
    public int? TmsEngine { get; set; }

    [JsonProperty("mt_engine")]
    [Display("MT engine")]
    public int? MtEngine { get; set; }

    [JsonProperty("private_tm_key")]
    [Display("Private TM key")]
    public string? TmKey { get; set; }

    public string? Subject { get; set; }

    [JsonProperty("segmentation_rule")]
    [Display("Segmentation rule")]
    public string? SegmentationRule { get; set; }

    [JsonProperty("owner_email")]
    [Display("Owner email")]
    public string? OwnerEmail { get; set; }

    [JsonProperty("id_team")]
    [Display("Id team")]
    public string? IdTeam { get; set; }

    public string? LexiQa { get; set; }

    [Display("Speech-to-text")] public int? Speech2Text { get; set; }

    [JsonProperty("get_public_matches")]
    [Display("Get public matches")]
    public string? GetPublicMatches { get; set; }

    [JsonProperty("pretranslate_100")]
    [Display("Pretranslate 100")]
    public int? Pretranslate100 { get; set; }

    public string? Metadata { get; set; }
}