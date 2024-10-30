using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Request.Job;

public class JobRequest
{
    [Display("Job ID and password")]
    public string JobId { get; set; } = string.Empty;
}