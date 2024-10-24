using Apps.Matecat.Constants;
using Apps.Matecat.Models.Response.Glossary;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using DocumentFormat.OpenXml.Vml.Office;
using RestSharp;

namespace Apps.Matecat.DataSourceHandlers;

public class TranslationMemoryDataHandler : BaseInvocable, IAsyncDataSourceHandler
{
    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;
    
    public TranslationMemoryDataHandler(InvocationContext invocationContext) : base(invocationContext)
    {
    }

    public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, 
        CancellationToken cancellationToken)
    {
        var request = new MatecatRequest(ApiEndpoints.TranslationMemories, Method.Get, Creds);
        var items = await new MatecatClient().ExecuteWithHandling<TranslationMemoriesResponse>(request);

        var privateKeys = items.PrivateKeys != null ? items
            .PrivateKeys
            .Where(item => context.SearchString is null ||
                        item.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(item => item.Key, item => item.Name + " (Private)") : new Dictionary<string, string>();

        var sharedKeys = items.SharedKeys != null ? items
            .SharedKeys
            .Where(item => context.SearchString is null ||
                        item.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
            .ToDictionary(item => item.Key, item => item.Name) : new Dictionary<string, string>();

        return new List<Dictionary<string, string>>() { privateKeys, sharedKeys }.SelectMany(dict => dict).ToDictionary();
    }
}