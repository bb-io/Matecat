﻿using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request;
using Apps.Matecat.Models.Request.Glossary;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using Blackbird.Applications.SDK.Extensions.FileManagement.Interfaces;
using Blackbird.Applications.Sdk.Glossaries.Utils.Converters;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos;
using Blackbird.Applications.Sdk.Utils.Extensions.Files;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class GlossaryActions : BaseInvocable
{
    #region Fields

    private readonly MatecatClient _client;
    private readonly IFileManagementClient _fileManagementClient;

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    public GlossaryActions(InvocationContext invocationContext, IFileManagementClient fileManagementClient) 
        : base(invocationContext)
    {
        _client = new();
        _fileManagementClient = fileManagementClient;
    }

    #endregion
    
    #region Actions

    [Action("Import glossary", Description = "Import a glossary, optionally specifying an existing Translation " +
                                             "Memory (TM) key to update it with the provided glossary")]
    public async Task ImportGlossary([ActionParameter] ImportGlossaryRequest input)
    {
        await using var glossaryStream = await _fileManagementClient.DownloadAsync(input.Glossary);
        var glossary = await glossaryStream.ConvertFromTbx();
        
        await using var excelStream = await glossary.ToMatecatExcelGlossary(Creds);
        var excelBytes = await excelStream.GetByteData();

        var request = new MatecatRequest(ApiEndpoints.ImportGlossary, Method.Post, Creds);
        var fileName = $"{input.GlossaryName ?? glossary.Title}.xlsx";
        
        request.AddFile("files", excelBytes, fileName);
        request.AddParameter("name", fileName);

        if (input.TranslationMemoryKey != null)
            request.AddParameter("tm_key", input.TranslationMemoryKey);

        await _client.ExecuteWithHandling(request);
    }
    
    #endregion
}