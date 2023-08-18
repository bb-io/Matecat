using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Team;

public class User
{
    [JsonProperty("uid")]
    [Display("UID")]
    public string Uid { get; set; }

    [JsonProperty("first_name")]
    [Display("First name")]
    public string FirstName { get; set; }

    [JsonProperty("last_name")]
    [Display("Last name")]
    public string LastName { get; set; }

    [JsonProperty("email")]
    [Display("Email")]
    public string Email { get; set; }

    [JsonProperty("has_password")]
    [Display("Has password")]
    public bool HasPassword { get; set; }
}