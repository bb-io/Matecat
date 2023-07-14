using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Project;

public class Project
{
    [JsonProperty("ID")]
    public long Id { get; set; }

    [JsonProperty("password")]
    public string Password { get; set; }

    [JsonProperty("name")]
    public string Name { get; set; }

    [JsonProperty("id_team")]
    [Display("Team ID")]
    public long IdTeam { get; set; }

    [JsonProperty("id_assignee")]
    [Display("Assignee ID")]
    public long IdAssignee { get; set; }

    [JsonProperty("create_date")]
    [Display("Create date")]
    public DateTime CreateDate { get; set; }

    [JsonProperty("project_slug")]
    public string ProjectSlug { get; set; }

    [JsonProperty("features")]
    public string Features { get; set; }

    [JsonProperty("jobs")]
    public List<Job.Job> Jobs { get; set; }
}