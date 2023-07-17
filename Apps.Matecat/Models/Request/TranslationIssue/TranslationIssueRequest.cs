using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Request.TranslationIssue;

public class TranslationIssueRequest
{
    [Display("Job ID and password")]
    public string JobId { get; set; }    
    
    [Display("Segment ID")]
    public string SegmentId { get; set; }
    
    [Display("Issue ID")]
    public string IssueId { get; set; }
}