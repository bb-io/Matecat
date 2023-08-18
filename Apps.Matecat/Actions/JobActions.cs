using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Response;
using Apps.Matecat.Models.Response.Job;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class JobActions
{
    #region Fields

    private readonly MatecatClient _client;

    #endregion

    #region Constructors

    public JobActions()
    {
        _client = new();
    }

    #endregion

    #region Actions

    [Action("Download translation as ZIP", Description = "Download job translation as ZIP")]
    public async Task<FileResponse> DownloadTranslationAsZip(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Translation}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        var response = await _client.ExecuteWithHandling(request);

        return new(response.RawBytes);
    }
    
    [Action("Download translation", Description = "Download job translation")]
    public async Task<FilesResponse> DownloadTranslation(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var archive = (await DownloadTranslationAsZip(creds, jobId)).File;
        var files = archive.GetFilesFromZip().ToList();
        
        return new(files);
    }

    [Action("Download job TMX", Description = "Download TMX of a job")]
    public async Task<FileResponse> DownloadTmx(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")] string jobId)
    {
        var endpoint = $"{ApiEndpoints.Tmx}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        var response = await _client.ExecuteWithHandling(request);
        return new(response.RawBytes);
    }

    [Action("Get job", Description = "Get all information about a Job")]
    public async Task<JobChunks> GetJob(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        var response = await _client.ExecuteWithHandling<JobResponse>(request);
        return response.Job;
    }

    [Action("Cancel job", Description = "Cancel a job")]
    public Task CancelJob(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/cancel";
        var request = new MatecatRequest(endpoint, Method.Post, creds);

        return _client.ExecuteWithHandling(request);
    }

    [Action("Archive job", Description = "Archive a job")]
    public Task ArchiveJob(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/archive";
        var request = new MatecatRequest(endpoint, Method.Post, creds);

        return _client.ExecuteWithHandling(request);
    }

    [Action("Activate job", Description = "Activate a job")]
    public Task ActivateJob(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/active";
        var request = new MatecatRequest(endpoint, Method.Post, creds);

        return _client.ExecuteWithHandling(request);
    }

    [Action("Get job segments comments", Description = "Gets the list of comments on all job segments")]
    public Task<CommentsResponse> GetSegmentComments(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId,
        [ActionParameter] [Display("From ID")] string? fromId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/comments";

        if (fromId is not null)
            endpoint += $"?from_id={fromId}";

        var request = new MatecatRequest(endpoint, Method.Get, creds);

        return _client.ExecuteWithHandling<CommentsResponse>(request);
    }

    #endregion
}