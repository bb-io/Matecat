using System.Net.Mime;
using Apps.Matecat.Constants;
using Apps.Matecat.Models.Response.File;
using Apps.Matecat.Models.Response.Job;
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
public class JobActions : BaseInvocable
{
    #region Fields

    private readonly MatecatClient _client;
    private readonly IFileManagementClient _fileManagementClient;

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    public JobActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext)
    {
        _client = new();
        _fileManagementClient = fileManagementClient;
    }

    #endregion

    #region Actions

    [Action("Download translation as ZIP", Description = "Download job translation as ZIP")]
    public async Task<FileResponse> DownloadTranslationAsZip(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Translation}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);
        var response = await _client.ExecuteWithHandling(request);

        using var stream = new MemoryStream(response.RawBytes);
        var translation = await _fileManagementClient.UploadAsync(stream,
            response.ContentType ?? MediaTypeNames.Application.Octet, $"{jobId}_translation");
        return new(translation);
    }

    [Action("Download translation", Description = "Download job translation")]
    public async Task<FilesResponse> DownloadTranslation(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var archive = (await DownloadTranslationAsZip(jobId)).File;
        var archiveStream = await _fileManagementClient.DownloadAsync(archive);
        var zipEntries = await archiveStream.GetFilesFromZip();
        var files = (await Task.WhenAll(zipEntries
            .Select(async file =>
                await _fileManagementClient.UploadAsync(file.FileStream, MediaTypeNames.Application.Octet,
                    file.UploadName)))).ToList();

        return new(files);
    }

    [Action("Download job TMX", Description = "Download TMX of a job")]
    public async Task<FileResponse> DownloadTmx(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Tmx}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);
        var response = await _client.ExecuteWithHandling(request);
        
        using var stream = new MemoryStream(response.RawBytes);
        var file = await _fileManagementClient.UploadAsync(stream, 
            response.ContentType ?? MediaTypeNames.Application.Octet, $"{jobId}.tmx");
        return new(file);
    }

    [Action("Get job", Description = "Get all information about a Job")]
    public async Task<JobChunks> GetJob(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        var response = await _client.ExecuteWithHandling<JobResponse>(request);
        return response.Job;
    }

    [Action("Cancel job", Description = "Cancel a job")]
    public Task CancelJob([ActionParameter] [Display("Job ID and password")] string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/cancel";
        var request = new MatecatRequest(endpoint, Method.Post, Creds);

        return _client.ExecuteWithHandling(request);
    }

    [Action("Archive job", Description = "Archive a job")]
    public Task ArchiveJob([ActionParameter] [Display("Job ID and password")] string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/archive";
        var request = new MatecatRequest(endpoint, Method.Post, Creds);

        return _client.ExecuteWithHandling(request);
    }

    [Action("Activate job", Description = "Activate a job")]
    public Task ActivateJob([ActionParameter] [Display("Job ID and password")] string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/active";
        var request = new MatecatRequest(endpoint, Method.Post, Creds);

        return _client.ExecuteWithHandling(request);
    }

    [Action("Get job segments comments", Description = "Gets the list of comments on all job segments")]
    public Task<CommentsResponse> GetSegmentComments(
        [ActionParameter] [Display("Job ID and password")]
        string jobId,
        [ActionParameter] [Display("From ID")] string? fromId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/comments";

        if (fromId is not null)
            endpoint += $"?from_id={fromId}";

        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        return _client.ExecuteWithHandling<CommentsResponse>(request);
    }

    #endregion
}