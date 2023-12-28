using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request;
using Apps.Matecat.Models.Request.Project;
using Apps.Matecat.Models.Response.Project;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class ProjectActions : BaseInvocable
{
    #region Fields

    private readonly MatecatClient _client;
    private readonly IFileManagementClient _fileManagementClient;

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    public ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext)
    {
        _client = new();
        _fileManagementClient = fileManagementClient;
    }

    #endregion

    #region Actions
    
    [Action("Create project", Description = "Create new project in detached mode")]
    public async Task<CreateProjectResponse> CreateProject(
        [ActionParameter] UploadFileRequest fileData,
        [ActionParameter] CreateProjectRequest requestData)
    {
        var fileStream = await _fileManagementClient.DownloadAsync(fileData.File);
        var fileBytes = await fileStream.GetByteData();
        var request = new MatecatRequest(ApiEndpoints.NewProject, Method.Post, Creds)
            .WithFormData(requestData, isMultipartFormData: true)
            .WithFile(fileBytes, fileData.FileName ?? fileData.File.Name);

        return await _client.ExecuteWithHandling<CreateProjectResponse>(request);
    }    
    
    [Action("Get project", Description = "Retrieve information on the specified Project")]
    public async Task<Project> GetProject(
        [ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        var response = await _client.ExecuteWithHandling<ProjectResponse>(request);
        return response.Project;
    }
    
    [Action("Cancel project", Description = "Cancel a project")]
    public Task CancelProject([ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}/cancel";
        var request = new MatecatRequest(endpoint, Method.Post, Creds);

        return _client.ExecuteWithHandling(request);
    }    
    
    [Action("Archive project", Description = "Archive a project")]
    public Task ArchiveProject([ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}/archive";
        var request = new MatecatRequest(endpoint, Method.Post, Creds);

        return _client.ExecuteWithHandling(request);
    }
    
    [Action("Activate project", Description = "Activate a project")]
    public Task ActivateProject([ActionParameter] [Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}/active";
        var request = new MatecatRequest(endpoint, Method.Post, Creds);

        return _client.ExecuteWithHandling(request);
    }
    
    #endregion
}