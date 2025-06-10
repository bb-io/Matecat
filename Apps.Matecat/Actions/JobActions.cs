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
using System.IO;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request.Job;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;
using Blackbird.Applications.Sdk.Utils.Utilities;
using DocumentFormat.OpenXml.Presentation;
using Blackbird.Applications.Sdk.Common.Exceptions;

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

    [Action("Download job translated files", Description = "Download all translated files for this job")]
    public async Task<FilesResponse> DownloadTranslations(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"/api/v3{ApiEndpoints.Translation}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);
        var response = await _client.ExecuteWithHandling(request);

        using var stream = new MemoryStream(response.RawBytes);
        if (response.ContentType == MediaTypeNames.Application.Zip)
        {            
            var zipEntries = await stream.GetFilesFromZip();
            var files = (await Task.WhenAll(zipEntries
                .Select(async file =>
                    await _fileManagementClient.UploadAsync(file.FileStream, MimeTypes.GetMimeType(file.UploadName),
                        file.UploadName)))).ToList();

            return new(files);
        }

        var disposition = response.ContentHeaders?.ToList()?.Find(x => x.Name == "Content-Disposition")?.Value?.ToString();
        var fileName = disposition != null ? new ContentDisposition(disposition).FileName : jobId.Split('/')[0];

        var translation = await _fileManagementClient.UploadAsync(stream, MimeTypes.GetMimeType(fileName), fileName);
        return new FilesResponse(new List<FileReference> { translation });
    }

    [Action("Download job file as TMX", Description = "Download TMX of a job")]
    public async Task<FileResponse> DownloadTmx(
        [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var endpoint = $"{ApiEndpoints.Tmx}/{jobId}";
        var request = new MatecatRequest(endpoint, Method.Get, Creds);
        var response = await _client.ExecuteWithHandling(request);
        
        using var stream = new MemoryStream(response.RawBytes);
        var file = await _fileManagementClient.UploadAsync(stream, 
            response.ContentType ?? MediaTypeNames.Application.Xml, $"{jobId}.tmx");
        return new(file);
    }


    [Action("Download job file as XLIFF", Description = "Download XLIFF file of a job")]
    public async Task<FileResponse> DownloadXliff(
       [ActionParameter] [Display("Job ID and password")]
        string jobId)
    {
        var jobEndpoint = $"{ApiEndpoints.Jobs}/{jobId}";
        var jobRequest = new MatecatRequest(jobEndpoint, Method.Get, Creds);
        var jobResponse = await _client.ExecuteWithHandling(jobRequest);
        var jobData = JsonConvert.DeserializeObject<JobResponse>(jobResponse.Content);
        var xliffUrl = jobData.Job.Chunks?.FirstOrDefault()?.Urls?.XliffDownloadUrl;

        if (string.IsNullOrEmpty(xliffUrl))
        {
            throw new PluginApplicationException("XLIFF download URL not found in the response.");
        }

        var filename = jobId.Replace("/", "_") + ".xliff";
        var file = await FileDownloader.DownloadFileBytes(xliffUrl);
        var fileRef = await _fileManagementClient.UploadAsync(file.FileStream, file.ContentType ?? MediaTypeNames.Application.Xml, filename);
        return new FileResponse(fileRef);
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
    
    [Action("Assign job", Description = "Assign a job to a translator")]
    public async Task<TranslatorJobResponse> AssignJob([ActionParameter] AssignJobRequest assignJobRequest)
    {
        var endpoint = $"{ApiEndpoints.Jobs}/{assignJobRequest.JobId}/translator";
        var request = new MatecatRequest(endpoint, Method.Post, Creds)
            .WithFormData(new
            {
                email = assignJobRequest.Email,
                delivery_date = new DateTimeOffset(assignJobRequest.DeliveryDate).ToUnixTimeSeconds(),
                timezone = assignJobRequest.Timezone
            }, isMultipartFormData: true);

        var result = await _client.ExecuteWithHandling<AssignJobResponse>(request);
        result.Job.Translator.SetDeliveryDate();
        
        return result.Job;
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