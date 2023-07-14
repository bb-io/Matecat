using Apps.Matecat.Constants;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Connections;

namespace Apps.Matecat.Connections;

public class ConnectionDefinition : IConnectionDefinition
{
    public IEnumerable<ConnectionPropertyGroup> ConnectionPropertyGroups => new List<ConnectionPropertyGroup>
    {
        new()
        {
            Name = "Developer API key",
            AuthenticationType = ConnectionAuthenticationType.Undefined,
            ConnectionUsage = ConnectionUsage.Actions,
            ConnectionProperties = new List<ConnectionProperty>
            {
                new(CredsNames.ApiKey)
                {
                    DisplayName = "API Key"
                },
                new(CredsNames.ApiSecret)
                {
                    DisplayName = "API Secret"
                }
            }
        }
    };

    public IEnumerable<AuthenticationCredentialsProvider> CreateAuthorizationCredentialsProviders(
        Dictionary<string, string> values)
    {
        var key = values.First(x => x.Key == CredsNames.ApiKey);
        yield return new(AuthenticationCredentialsRequestLocation.None, key.Key, key.Value);

        var secret = values.First(x => x.Key == CredsNames.ApiSecret);
        yield return new(AuthenticationCredentialsRequestLocation.None, secret.Key, secret.Value);
        
    }
}