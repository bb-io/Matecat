using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request;
using Apps.Matecat.Models.Request.Project;
using Apps.Matecat.Models.Response.Project;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class ProjectActions
{
    #region Fields

    private readonly MatecatClient _client;

    #endregion

    #region Constructors

    public ProjectActions()
    {
        _client = new();
    }

    #endregion

    #region Actions
    
    [Action("Create project", Description = "Create new project in detached mode")]
    public Task<CreateProjectResponse> CreateProject(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] UploadFileRequest fileData,
        [ActionParameter] CreateProjectRequest requestData)
    {
        var request = new MatecatRequest(ApiEndpoints.NewProject, Method.Post, creds)
            .WithFormData(requestData, isMultipartFormData: true)
            .WithFile(fileData.File, fileData.FileName);

        return _client.ExecuteWithHandling<CreateProjectResponse>(request);
    }    
    
    [Action("Get project", Description = "Retrieve information on the specified Project")]
    public Task<Project> GetProject(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        return _client.ExecuteWithHandling<Project>(request);
    }
    
    [Action("Cancel project", Description = "Cancel a project")]
    public Task CancelProject(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}/cancel";
        var request = new MatecatRequest(endpoint, Method.Post, creds);

        return _client.ExecuteWithHandling(request);
    }    
    
    [Action("Archive project", Description = "Archive a project")]
    public Task ArchiveProject(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}/archive";
        var request = new MatecatRequest(endpoint, Method.Post, creds);

        return _client.ExecuteWithHandling(request);
    }
    
    [Action("Activate project", Description = "Activate a project")]
    public Task ActivateProject(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}/active";
        var request = new MatecatRequest(endpoint, Method.Post, creds);

        return _client.ExecuteWithHandling(request);
    }
    
    #endregion
}