using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request.TranslationIssue;
using Apps.Matecat.Models.Response.TranslationIssue;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class TranslationIssueActions : BaseInvocable
{
    #region Fields

    private readonly MatecatClient _client;

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    public TranslationIssueActions(InvocationContext invocationContext) : base(invocationContext)
    {
        _client = new();
    }

    #endregion

    #region Actions

    [Action("Get translation issues", Description = "Gets project translation issues")]
    public Task<TranslationIssuesResponse> GetTranslationIssues(
        [ActionParameter] [Display("Job ID and password")] string jobId)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{jobId}/translation-issues";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        return _client.ExecuteWithHandling<TranslationIssuesResponse>(request);
    }

    [Action("Create translation issue", Description = "Create project translation issue")]
    public async Task<TranslationIssue> CreateTranslationIssue(
        [ActionParameter] CreateTranslationIssueRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/{requestData.Password}/segments"
                       + $"/{requestData.SegmentId}/translation-issues";
    
        var request = new MatecatRequest(endpoint, Method.Post, Creds)
            .WithFormData(requestData);
    
        var response = await _client.ExecuteWithHandling<TranslationIssueResponse>(request);
        return response.Issue;
    }
    
    [Action("Delete translation issue", Description = "Delete project translation issue")]
    public Task DeleteTranslationIssue([ActionParameter] TranslationIssueRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/segments"
                       + $"/{requestData.SegmentId}/translation-issues/{requestData.IssueId}";
    
        var request = new MatecatRequest(endpoint, Method.Delete, Creds);
    
        return _client.ExecuteWithHandling(request);
    }

    [Action("Get translation issue comments", Description = "Get project translation issue comments")]
    public Task<TranslationIssueCommentsResponse> GetTranslationIssueComments(
        [ActionParameter] TranslationIssueRequest requestData)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/segments"
                       + $"/{requestData.SegmentId}/translation-issues/{requestData.IssueId}/comments";

        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        return _client.ExecuteWithHandling<TranslationIssueCommentsResponse>(request);
    }

    // [Action("Add translation issue comment", Description = "Add project translation issue comment")]
    // public async Task<TranslationIssueCommentV3> AddTranslationIssueCommentRequest(
    //     
    //     [ActionParameter] AddTranslationIssueCommentRequest requestData)
    // {
    //     var endpoint = $"{ApiEndpoints.Jobs}/{requestData.JobId}/{requestData.Password}/segments"
    //                    + $"/{requestData.SegmentId}/translation-issues/{requestData.IssueId}/comments";
    //
    //     var request = new MatecatRequest(endpoint, Method.Post, Creds)
    //         .WithFormData(requestData);
    //
    //     var response = await _client.ExecuteWithHandling<TranslationIssueCommentResponse>(request);
    //     return response.Comment;
    // }

    #endregion
}