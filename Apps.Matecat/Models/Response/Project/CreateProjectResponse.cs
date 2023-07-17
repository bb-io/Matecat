using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Project;

public class CreateProjectResponse
{
    [JsonProperty("id_project")]
    [Display("Project ID")]
    public string ProjectId { get; set; }

    [JsonProperty("project_pass")]
    [Display("Project password")]
    public string ProjectPassword { get; set; }
}