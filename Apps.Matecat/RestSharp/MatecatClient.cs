using Apps.Matecat.Models.Response.Error;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Newtonsoft.Json;
using RestSharp;
using System.Net;

namespace Apps.Matecat.RestSharp;

public class MatecatClient() : RestClient(new RestClientOptions { BaseUrl = new("https://www.matecat.com") })
{
    private const int MaxRetries = 5;
    private const int InitialDelayMs = 1000;

    public async Task<T> ExecuteWithHandling<T>(RestRequest request)
    {
        var response = await ExecuteWithHandling(request);
        return JsonConvert.DeserializeObject<T>(response.Content!)!;
    }

    public async Task<RestResponse> ExecuteWithHandling(RestRequest request)
    {
        int delay = InitialDelayMs;
        RestResponse? response = null;

        for (int attempt = 1; attempt <= MaxRetries; attempt++)
        {
            response = await ExecuteAsync(request);

            if (response.IsSuccessful)
                return response;

            if (attempt < MaxRetries &&
                (response.StatusCode == HttpStatusCode.InternalServerError ||
                 response.StatusCode == HttpStatusCode.ServiceUnavailable ||
                 response.StatusCode == HttpStatusCode.BadRequest ||
                 response.StatusCode == HttpStatusCode.TooManyRequests))
            {
                await Task.Delay(delay);
                delay *= 2;
                continue;
            }
            break;
        }
        throw ConfigureErrorException(response);
    }

    private Exception ConfigureErrorException(RestResponse response)
    {
        try
        {
            var errorsResponse = JsonConvert.DeserializeObject<ErrorsResponse>(response.Content!);
            if (errorsResponse?.Errors is not null && errorsResponse.Errors.Any())
                return GetMultipleErrors(errorsResponse);

            var error = JsonConvert.DeserializeObject<Error>(response.Content!);
            return new PluginApplicationException(error?.Message ?? response.StatusDescription!);
        }
        catch (JsonException)
        {
            return new PluginApplicationException($"Unexpected error format: {response.StatusCode} - {response.Content}");
        }
    }

    private Exception GetMultipleErrors(ErrorsResponse errorsResponse)
    {
        var messages = errorsResponse.Errors.Select(x => x.Message).ToArray();
        return new PluginApplicationException(string.Join(';', messages));
    }
}