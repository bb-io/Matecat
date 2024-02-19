using Apps.Matecat.Constants;
using Apps.Matecat.Models.Response.Glossary;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Matecat.DataSourceHandlers;

public class PrivateTranslationMemoryDataHandler : BaseInvocable, IAsyncDataSourceHandler
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;
    
    public PrivateTranslationMemoryDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, 
        CancellationToken cancellationToken)
    {
        var request = new MatecatRequest(ApiEndpoints.TranslationMemories, Method.Get, Creds);
        var items = await new MatecatClient().ExecuteWithHandling<TranslationMemoriesResponse>(request);

        return items
            .PrivateKeys
            .Where(item => context.SearchString is null ||
                        item.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(item => item.Key, item => item.Name);
    }
}