using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.TranslationIssue;

public class AddTranslationIssueCommentRequest
{
    [JsonProperty("id_job")]
    [Display("Job ID")]
    public long JobId { get; set; } 
    
    [JsonProperty("password")]
    [Display("Job password")]
    public string Password { get; set; }    
    
    [JsonProperty("id_segment")]
    [Display("Segment ID")]
    public long SegmentId { get; set; }
    
    [JsonProperty("id_issue")]
    
    [Display("Issue ID")]
    public long IssueId { get; set; }
    
    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }

    [JsonProperty("id_qa_entry")]
    [Display("QA entry ID")]
    public long IdQaEntry { get; set; }

    [JsonProperty("source_page")]
    [Display("Source page")]
    public int SourcePage { get; set; }

    [JsonProperty("uid")]
    [Display("UID")]
    public long Uid { get; set; }
}