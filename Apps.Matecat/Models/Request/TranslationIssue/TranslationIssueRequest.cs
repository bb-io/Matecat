using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.TranslationIssue;

public class TranslationIssueRequest
{
    [JsonProperty("id_job")]
    [Display("Job ID and password")]
    public string JobId { get; set; }    
    
    [JsonProperty("id_segment")]
    [Display("Segment ID")]
    public long SegmentId { get; set; }
    
    [JsonProperty("id_issue")]
    
    [Display("Issue ID")]
    public long IssueId { get; set; }
}