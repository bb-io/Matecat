using RestSharp;

namespace Apps.Matecat.RestSharp;

public class MatecatClient : RestClient
{
    public MatecatClient() : base(new RestClientOptions { BaseUrl = new("www.matecat.com/api") })
    {
    }

    public async Task<T> ExecuteWithHandling<T>(RestRequest request)
    {
        var response = await this.ExecuteAsync<T>(request);

        if (response.IsSuccessStatusCode)
            return response.Data;

        throw ConfigureErrorException(response);
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
        //TODO: Add error configuring
        throw new NotImplementedException();
    }
}