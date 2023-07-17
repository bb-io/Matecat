using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.Team;

public class CreateTeamRequest
{
    [JsonProperty("name")] public string Name { get; set; }
    [JsonProperty("type")] public string Type { get; set; }
    [JsonProperty("members")] public IEnumerable<string> Members { get; set; }
}