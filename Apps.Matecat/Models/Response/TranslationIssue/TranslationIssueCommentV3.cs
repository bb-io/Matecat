using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.TranslationIssue;

public class TranslationIssueCommentV3
{
    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }

    [JsonProperty("id_category")]
    [Display("Category ID")]
    public string CategoryId { get; set; }

    [JsonProperty("id_job")]
    [Display("Job ID")]
    public string JobId { get; set; }

    [JsonProperty("id_segment")]
    [Display("Segment ID")]
    public string SegmentId { get; set; }

    [JsonProperty("is_full_segment")]
    [Display("Is full segment")]
    public string IsFullSegment { get; set; }

    [JsonProperty("severity")]
    [Display("Severity")]
    public string Severity { get; set; }

    [JsonProperty("start_node")]
    [Display("Start node")]
    public int? StartNode { get; set; }

    [JsonProperty("start_offset")]
    [Display("Start offset")]
    public int? StartOffset { get; set; }

    [JsonProperty("end_node")]
    [Display("End node")]
    public int? EndNode { get; set; }

    [JsonProperty("end_offset")]
    [Display("End offset")]
    public int? EndOffset { get; set; }

    [JsonProperty("translation_version")]
    [Display("Translation version")]
    public int? TranslationVersion { get; set; }

    [JsonProperty("target_text")]
    [Display("Target text")]
    public string TargetText { get; set; }

    [JsonProperty("penalty_points")]
    [Display("Penalty points")]
    public string PenaltyPoints { get; set; }

    [JsonProperty("rebutted_at")]
    [Display("Rebutted at")]
    public string RebuttedAt { get; set; }

    [JsonProperty("diff")]
    [Display("Diff")]
    public string Diff { get; set; }

    [JsonProperty("revision_number")]
    [Display("Revision number")]
    public int RevisionNumber { get; set; }
}