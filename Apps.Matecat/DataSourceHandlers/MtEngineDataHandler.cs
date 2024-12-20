﻿using Apps.Matecat.Constants;
using Apps.Matecat.Models.Response;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Dynamic;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.Sdk.Common;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Apps.Matecat.Dto;

namespace Apps.Matecat.DataSourceHandlers
{
    public class MtEngineDataHandler : BaseInvocable, IAsyncDataSourceHandler
    {
        private IEnumerable<AuthenticationCredentialsProvider> Creds =>
            InvocationContext.AuthenticationCredentialsProviders;

        public MtEngineDataHandler(InvocationContext invocationContext) : base(invocationContext)
        {
        }

        public async Task<Dictionary<string, string>> GetDataAsync(DataSourceContext context, CancellationToken cancellationToken)
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

            return new List<Dictionary<string, string>>() { standard, extra }.SelectMany(dict => dict).ToDictionary();
        }
    }
}
