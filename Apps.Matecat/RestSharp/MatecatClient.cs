using Apps.Matecat.Models.Response.Error;
using Blackbird.Applications.Sdk.Common.Exceptions;
using Newtonsoft.Json;
using RestSharp;

namespace Apps.Matecat.RestSharp;

public class MatecatClient() : RestClient(new RestClientOptions { BaseUrl = new("https://www.matecat.com") })
{
    public async Task<T> ExecuteWithHandling<T>(RestRequest request)
    {
        var response = await ExecuteWithHandling(request);
        return JsonConvert.DeserializeObject<T>(response.Content!)!;
    }    
    
    public async Task<RestResponse> ExecuteWithHandling(RestRequest request)
    {
        var response = await ExecuteAsync(request);
        if (response.IsSuccessStatusCode)
            return response;

        throw ConfigureErrorException(response);
    }

    private Exception ConfigureErrorException(RestResponse response)
    {
        var errorsResponse = JsonConvert.DeserializeObject<ErrorsResponse>(response.Content);

        if (errorsResponse.Errors is not null)
            return GetMultipleErrors(errorsResponse);
        
        var error = JsonConvert.DeserializeObject<Error>(response.Content);
        return new PluginApplicationException(error.Message);
    }

    private Exception GetMultipleErrors(ErrorsResponse errorsResponse)
    {
        var messages = errorsResponse.Errors.Select(x => x.Message).ToArray();
        return new PluginApplicationException(string.Join(';', messages));
    }
}