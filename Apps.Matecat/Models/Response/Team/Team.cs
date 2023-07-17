using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Team;

public class Team
{
    [JsonProperty("id")]
    [Display("ID")]
    public int Id { get; set; }

    [JsonProperty("name")]
    [Display("Name")]
    public string Name { get; set; }

    [JsonProperty("type")]
    [Display("Type")]
    public string Type { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("created_by")]
    [Display("Created by")]
    public int CreatedBy { get; set; }
}