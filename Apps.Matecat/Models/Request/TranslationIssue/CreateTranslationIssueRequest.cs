using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Request.TranslationIssue;

public class CreateTranslationIssueRequest
{
    [Display("Job ID")] public string IdJob { get; set; }

    public string Password { get; set; }
    public string IdSegment  { get; set; }
}