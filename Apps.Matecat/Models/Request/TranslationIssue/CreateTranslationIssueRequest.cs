using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.TranslationIssue;

public class CreateTranslationIssueRequest
{
    [JsonProperty("id_job")]
    [Display("Job ID")]
    public long JobId { get; set; }

    [JsonProperty("password")]
    [Display("Password")]
    public string Password { get; set; }

    [JsonProperty("id_segment")]
    [Display("Segment ID")]
    public long SegmentId { get; set; }

    [JsonProperty("version_number")]
    [Display("Version Number")]
    public int VersionNumber { get; set; }

    [JsonProperty("id_category")]
    [Display("Category ID")]
    public long CategoryId { get; set; }

    [JsonProperty("severity")]
    [Display("Severity")]
    public string Severity { get; set; }

    [JsonProperty("translation_version")]
    [Display("Translation Version")]
    public int TranslationVersion { get; set; }

    [JsonProperty("target_text")]
    [Display("Target Text")]
    public string TargetText { get; set; }

    [JsonProperty("start_node")]
    [Display("Start Node")]
    public int StartNode { get; set; }

    [JsonProperty("start_offset")]
    [Display("Start Offset")]
    public int StartOffset { get; set; }

    [JsonProperty("end_node")]
    [Display("End Node")]
    public int EndNode { get; set; }

    [JsonProperty("end_offset")]
    [Display("End Offset")]
    public int EndOffset { get; set; }

    [JsonProperty("is_full_segment")]
    [Display("Is Full Segment")]
    public int IsFullSegment { get; set; }

    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }
}