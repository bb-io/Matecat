using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Project;

public class CreateProjectResponse
{
    [JsonProperty("id_project")]
    [Display("Project")]
    public string ProjectId { get; set; }

    [JsonProperty("project_pass")]
    [Display("Password")]
    public string ProjectPassword { get; set; }

    [Display("Project ID and password")] public string ProjectIdAndPassword => $"{ProjectId}/{ProjectPassword}";
}