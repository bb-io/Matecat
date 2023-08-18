using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class SegmentComment
{
    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }

    [JsonProperty("id_job")]
    [Display("Job ID")]
    public string IdJob { get; set; }

    [JsonProperty("id_segment")]
    [Display("Segment ID")]
    public string IdSegment { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public string CreatedAt { get; set; }

    [JsonProperty("email")]
    [Display("Email")]
    public string Email { get; set; }

    [JsonProperty("full_name")]
    [Display("Full name")]
    public string FullName { get; set; }

    [JsonProperty("uid")]
    [Display("UID")]
    public string Uid { get; set; }

    [JsonProperty("resolved_at")]
    [Display("Resolved at")]
    public string ResolvedAt { get; set; }

    [JsonProperty("source_page")]
    [Display("Source page")]
    public int SourcePage { get; set; }

    [JsonProperty("message_type")]
    [Display("Message type")]
    public int MessageType { get; set; }

    [JsonProperty("message")]
    [Display("Message")]
    public string Message { get; set; }
}