using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.TranslationIssue;

public class TranslationIssue
{
    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public string CreatedAt { get; set; }

    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }

    [JsonProperty("id_category")]
    [Display("Category ID")]
    public string IdCategory { get; set; }

    [JsonProperty("id_job")]
    [Display("Job ID")]
    public string IdJob { get; set; }

    [JsonProperty("id_segment")]
    [Display("Segment ID")]
    public string IdSegment { get; set; }

    [JsonProperty("is_full_segment")]
    [Display("Is full segment")]
    public string IsFullSegment { get; set; }

    [JsonProperty("severity")]
    [Display("Severity")]
    public string Severity { get; set; }

    [JsonProperty("start_node")]
    [Display("Start node")]
    public string StartNode { get; set; }

    [JsonProperty("start_offset")]
    [Display("Start offset")]
    public string StartOffset { get; set; }

    [JsonProperty("end_node")]
    [Display("End node")]
    public string EndNode { get; set; }

    [JsonProperty("end_offset")]
    [Display("End offset")]
    public string EndOffset { get; set; }

    [JsonProperty("translation_version")]
    [Display("Translation version")]
    public string TranslationVersion { get; set; }

    [JsonProperty("target_text")]
    [Display("Target text")]
    public string TargetText { get; set; }

    [JsonProperty("penality_points")]
    [Display("Penality points")]
    public string PenalityPoints { get; set; }

    [JsonProperty("rebutted_at")]
    [Display("Rebutted at")]
    public string RebuttedAt { get; set; }
}