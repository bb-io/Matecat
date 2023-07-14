using Apps.Matecat.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Matecat.RestSharp;

public class MatecatRequest : RestRequest
{
    public MatecatRequest(
        string endpoint,
        Method method,
        IEnumerable<AuthenticationCredentialsProvider> creds) : base(endpoint, method)
    {
        var key = creds.First(x => x.KeyName == CredsNames.ApiKey).Value;
        var secret = creds.First(x => x.KeyName == CredsNames.ApiSecret).Value;

        this.AddHeader("x-matecat-key", $"{key}-{secret}");
    }
}