using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Request.TranslationIssue;

public class TranslationIssueRequest
{
    [Display("Job ID and revision password")]
    public string JobId { get; set; }    
    
    [Display("Segment ID")]
    public string SegmentId { get; set; }
    
    [Display("Translation issue ID")]
    public string IssueId { get; set; }
}