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
using Apps.Matecat.Dto;
using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Common.Exceptions;

namespace Apps.Matecat.Actions;

[ActionList]
public class ProjectActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient)
    : BaseInvocable(invocationContext)
{
    #region Fields

    private readonly MatecatClient _client = new();

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    #endregion

    #region Actions
    
    [Action("Create project", Description = "Create new project in detached mode")]
    public async Task<Project> CreateProject(
        [ActionParameter] UploadFilesRequest fileData,
        [ActionParameter] CreateProjectRequest requestData)
    {
        var request = new MatecatRequest(ApiEndpoints.NewProject, Method.Post, Creds)
            .WithFormData(new NewProject(requestData), isMultipartFormData: true);

        foreach(var file in fileData.Files)
        {
            var fileStream = await fileManagementClient.DownloadAsync(file);
            var fileBytes = await fileStream.GetByteData();
            request = request.WithFile(fileBytes, file.Name);
        }

        var response = await _client.ExecuteWithHandling<CreateProjectResponse>(request);

        var creationStatusRequest = new MatecatRequest($"{ApiEndpoints.Projects}/{response.ProjectIdAndPassword}/creation_status", Method.Get, Creds);
        var creationStatus = new ProjectCreationStatus { Status = 202, Message = "" };        

        while(creationStatus.Status == 202)
        {
            creationStatus = await _client.ExecuteWithHandling<ProjectCreationStatus>(creationStatusRequest);
            Thread.Sleep(3000);
        }

        if (creationStatus.Status != 200)
        {
            throw new PluginApplicationException(creationStatus.Message);
        }

        return await GetProject(response.ProjectIdAndPassword);
    }    
    
    [Action("Get project", Description = "Retrieve information on the specified project")]
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