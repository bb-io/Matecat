using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class RevisePassword
{
    [JsonProperty("revision_number")]
    [Display("Revision number")]
    public int RevisionNumber { get; set; }
    
    public string Password { get; set; }
}