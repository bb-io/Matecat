using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.TranslationIssue;

public class TranslationIssueCommentV2
{
    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }

    [JsonProperty("uid")]
    [Display("UID")]
    public string Uid { get; set; }

    [JsonProperty("id_issue")]
    [Display("Issue ID")]
    public string IssueId { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreateDate { get; set; }

    [JsonProperty("message")]
    [Display("Message")]
    public string Message { get; set; }

    [JsonProperty("source_page")]
    [Display("Source page")]
    public int SourcePage { get; set; }
}