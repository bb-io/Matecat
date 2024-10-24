
using Apps.Matecat.Actions;
using Apps.Matecat.Constants;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;
using Blackbird.Applications.Sdk.Common.Invocation;
using DocumentFormat.OpenXml.Drawing;
using RestSharp;

namespace Apps.Matecat.Connections
{
    public class ConnectionValidator : IConnectionValidator
    {
        public async ValueTask<ConnectionValidationResponse> ValidateConnection(
       IEnumerable<AuthenticationCredentialsProvider> authProviders, CancellationToken cancellationToken)
        {
            var client = new MatecatClient();
            var request = new MatecatRequest(ApiEndpoints.Teams, Method.Get, authProviders);
            try
            {
                await client.ExecuteWithHandling(request);
                return new ConnectionValidationResponse
                {
                    IsValid = true,
                    Message = "Success"
                };
            }
            catch (Exception ex)
            {
                return new ConnectionValidationResponse
                {
                    IsValid = false,
                    Message = ex.Message
                };
            }
        }
    }
}
