using Apps.Matecat.DataSourceHandlers.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.Team;

public class CreateTeamRequest
{
    [JsonProperty("name")] public string Name { get; set; }

    [JsonProperty("type")]
    [DataSource(typeof(TeamTypeDataHandler))]
    public string Type { get; set; }

    [JsonProperty("members")] public IEnumerable<string> Members { get; set; }
}