using Apps.Matecat.Constants;
using Apps.Matecat.DataSourceHandlers.EnumDataHandlers;
using Apps.Matecat.Models.Response.Job;
using Apps.Matecat.Models.Response.Project;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common.Polling;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Matecat.Polling;

[PollingEventList]
public class PollingList : BaseInvocable
{
    private readonly MatecatClient _client;
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
    InvocationContext.AuthenticationCredentialsProviders;

    public PollingList(InvocationContext invocationContext) : base(invocationContext)
    {
        _client = new();
    }

    [PollingEvent("On analysis completed", "This event triggers when a project analysis completes")]
    public async Task<PollingEventResponse<string, Project>> OnAnalysisCompleted(
        PollingEventRequest<string> input, 
        [PollingEventParameter][Display("Project ID and password")] string projectId)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        var response = await _client.ExecuteWithHandling<ProjectResponse>(request);
        var project = response.Project;

        var allOkStatuses = new List<string> { AnalysisStatus.New, AnalysisStatus.Busy, AnalysisStatus.FastOk, AnalysisStatus.Done };

        return new()
        {
            FlyBird = project.Analysis.Status == AnalysisStatus.Done || !allOkStatuses.Contains(project.Analysis.Status),
            Result = project,
            Memory = project.Analysis.Status,
        };
    }

    [PollingEvent("On job status changed", "This event triggers when a job changes its derived status")]
    public async Task<PollingEventResponse<string, Job>> OnJobStatusChanged(
        PollingEventRequest<string> input, 
        [PollingEventParameter][Display("Project ID and password")] string projectId,
        [PollingEventParameter][Display("Job ID")] string jobId,
        [PollingEventParameter][Display("New status")][StaticDataSource(typeof(DerivedStatusDataHandler))] string? derivedStatus)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        var response = await _client.ExecuteWithHandling<ProjectResponse>(request);
        var project = response.Project;

        var job = project.Jobs.Find(x => x.Id == jobId);

        if (job == null) throw new Exception("A job with this ID was not found in this project.");

        return new()
        {
            FlyBird = derivedStatus == null ? (input.Memory != null && job.DerivedStatus != input.Memory) : job.DerivedStatus == derivedStatus,
            Result = job,
            Memory = job.DerivedStatus,
        };
    }

    [PollingEvent("On project status changed", "This event triggers when a project changes its derived status")]
    public async Task<PollingEventResponse<string, Project>> OnProjectStatusChanged(
    PollingEventRequest<string> input,
    [PollingEventParameter][Display("Project ID and password")] string projectId,
    [PollingEventParameter][Display("New status")][StaticDataSource(typeof(DerivedStatusDataHandler))] string? derivedStatus)
    {
        var endpoint = $"{ApiEndpoints.Projects}/{projectId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);

        var response = await _client.ExecuteWithHandling<ProjectResponse>(request);
        var project = response.Project;

        return new()
        {
            FlyBird = derivedStatus == null ? (input.Memory != null && project.DerivedStatus != input.Memory) : project.DerivedStatus == derivedStatus,
            Result = project,
            Memory = project.DerivedStatus,
        };
    }
}

