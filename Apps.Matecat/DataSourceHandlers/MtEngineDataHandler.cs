using Apps.Matecat.Constants;
using Apps.Matecat.Dto;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using RestSharp;

namespace Apps.Matecat.DataSourceHandlers
{
    public class MtEngineDataHandler(InvocationContext invocationContext)
        : BaseInvocable(invocationContext), IAsyncDataSourceItemHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
            InvocationContext.AuthenticationCredentialsProviders;

        public async Task<IEnumerable<DataSourceItem>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
        {
            var request = new MatecatRequest(ApiEndpoints.Engines, Method.Get, Creds);
            var items = await new MatecatClient().ExecuteWithHandling<EngineDto[]>(request);

            var standard = new Dictionary<string, string>()
            {
                {"0", "Disabled"},
                {"1", "MT from MyMemory (Default)"}
            };

            var extra = items
                .Where(x => context.SearchString is null ||
                            x.Name.Contains(context.SearchString, StringComparison.OrdinalIgnoreCase))
                .ToDictionary(x => x.Id.ToString(), x => x.Name);

            return new List<Dictionary<string, string>> { standard, extra }
                .SelectMany(dict => dict)
                .Select(x => new DataSourceItem(x.Key, x.Value));
        }
    }
}
