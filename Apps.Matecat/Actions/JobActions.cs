using System.Net.Mime;
using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Response.File;
using Apps.Matecat.Models.Response.Job;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Matecat.Actions;

[ActionList]
public class JobActions : BaseInvocable
{
    #region Fields

    private readonly MatecatClient _client;

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    public JobActions(InvocationContext invocationContext) : base(invocationContext)
    {
        _client = new();
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

        return new(new(response.RawBytes)
        {
            Name = $"{jobId}_translation",
            ContentType = response.ContentType ?? MediaTypeNames.Application.Octet
        });
    }

    [Action("Download translation", Description = "Download job translation")]
    public async Task<FilesResponse> DownloadTranslation(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var archive = (await DownloadTranslationAsZip(jobId)).File;
        var files = archive.Bytes.GetFilesFromZip().ToList();

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
        return new(new(response.RawBytes)
        {
            Name = $"{jobId}.tmx",
            ContentType = response.ContentType ?? MediaTypeNames.Application.Octet
        });
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