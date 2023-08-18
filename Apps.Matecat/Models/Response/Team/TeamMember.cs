using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Team;

public class TeamMember
{
    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }
    
    [JsonProperty("id_team")]
    [Display("Team ID")]
    public string IdTeam { get; set; }
    
    [JsonProperty("user")]
    [Display("User")]
    public User User { get; set; }
}