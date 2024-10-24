using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Project;

public class CreateProjectResponse
{
    [JsonProperty("id_project")]
    [DefinitionIgnore]
    public string ProjectId { get; set; }

    [JsonProperty("project_pass")]
    [DefinitionIgnore]
    public string ProjectPassword { get; set; }

    [Display("Project ID and password")] 
    public string ProjectIdAndPassword => $"{ProjectId}/{ProjectPassword}";
}