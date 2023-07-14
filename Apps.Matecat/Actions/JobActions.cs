using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request.Job;
using Apps.Matecat.Models.Response;
using Apps.Matecat.Models.Response.Job;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Matecat.Actions;

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

    [Action("Download translation", Description = "Download job translation")]
    public async Task<TranslationResponse> DownloadTranslation(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Translation}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        var response = await _client.ExecuteWithHandling(request);

        return new(response.Content);
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

    [Action("Get segment comments", Description = "Gets the list of comments on all job segments")]
    public async Task<CommentsResponse> GetSegmentComments(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId,
        [ActionParameter] [Display("From ID")] int? fromId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/comments";

        if (fromId is not null)
            endpoint += $"?from_id={fromId}";

        var request = new MatecatRequest(endpoint, Method.Get, creds);

        var response = await _client.ExecuteWithHandling<List<SegmentComment>>(request);

        return new(response);
    }

    [Action("Get translation issues", Description = "Gets project translation issues")]
    public async Task<TranslationIssuesResponse> GetTranslationIssues(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/translation-issues";

        var request = new MatecatRequest(endpoint, Method.Get, creds);

        var response = await _client.ExecuteWithHandling<List<TranslationIssue>>(request);

        return new(response);
    }

    [Action("Create translation issue", Description = "Create project translation issue")]
    public Task<TranslationIssue> CreateTranslationIssue(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] CreateTranslationIssueRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.IdJob}/{requestData.Password}/segments"
                       + $"/{requestData.IdSegment}/translation-issues";

        var request = new MatecatRequest(endpoint, Method.Post, creds)
            .WithFormData(requestData);

        return _client.ExecuteWithHandling<TranslationIssue>(request);
    }

    [Action("Delete translation issue", Description = "Delete project translation issue")]
    public Task DeleteTranslationIssue(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] TranslationIssueRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/segments"
                       + $"/{requestData.SegmentId}/translation-issues/{requestData.IssueId}";

        var request = new MatecatRequest(endpoint, Method.Delete, creds);

        return _client.ExecuteWithHandling(request);
    }
    
    [Action("Get translation issue comments", Description = "Get project translation issue comments")]
    public Task<object> GetTranslationIssueComments(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] TranslationIssueRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/segments"
                       + $"/{requestData.SegmentId}/translation-issues/{requestData.IssueId}/comments";

        var request = new MatecatRequest(endpoint, Method.Get, creds);

        //TODO: Add deserialization model
        return _client.ExecuteWithHandling<object>(request);
    }
    
    [Action("Add translation issue comment", Description = "Add project translation issue comment")]
    public Task<object> AddTranslationIssueCommentRequest(
        IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] AddTranslationIssueCommentRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/segments"
                       + $"/{requestData.SegmentId}/translation-issues/{requestData.IssueId}/comments";

        var request = new MatecatRequest(endpoint, Method.Post, creds)
            .WithFormData(requestData);

        //TODO: Add deserialization model
        return _client.ExecuteWithHandling<object>(request);
    }

    #endregion
}