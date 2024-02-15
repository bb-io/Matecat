using Apps.Matecat.Constants;
using Apps.Matecat.Models.Response;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Glossaries.Utils.Dtos;
using DocumentFormat.OpenXml;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Spreadsheet;
using RestSharp;

namespace Apps.Matecat.Extensions;

public static class GlossaryExtensions
{
    #region Constants

    const string Domain = "Domain";
    const string Definition = "Definition";
    const string Notes = "Notes";
    const string ExampleOfUse = "Example of use";

    #endregion

    #region Import
    
    public static async Task<MemoryStream> ToMatecatExcelGlossary(this Glossary glossary,
        IEnumerable<AuthenticationCredentialsProvider> credentials)
    {
        var targetGlossaryLanguageCodes = await GetTargetGlossaryLanguageCodes(glossary, credentials);
        
        var languageRelatedHeaders = GenerateLanguageHeaders(targetGlossaryLanguageCodes);

        var rows = new List<List<string>>
            { new(new[] { Domain, Definition }.Concat(languageRelatedHeaders)) };
        
        foreach (var entry in glossary.ConceptEntries)
        {
            var languageRelatedValues = ((IEnumerable<string>)targetGlossaryLanguageCodes
                .SelectMany(languageCode =>
                    languageRelatedHeaders
                        .Select(column => GetColumnValue(column, entry, languageCode)))
                .Where(value => value != null))
                .ToList();
            
            rows.Add(new List<string>(new[]
            {
                entry.Domain ?? string.Empty,
                entry.Definition ?? string.Empty
            }.Concat(languageRelatedValues)));
        }

        var headers = rows[0];
        
        for (var i = 0; i < headers.Count; i++)
        {
            if (headers[i].StartsWith(Notes))
                headers[i] = Notes;
            
            if (headers[i].StartsWith(ExampleOfUse))
                headers[i] = ExampleOfUse;
        }

        var excelStream = CreateExcelFile(rows, glossary.Title ?? Guid.NewGuid().ToString());
        return excelStream;
    }

    #region Utils

    private static async Task<string[]> GetTargetGlossaryLanguageCodes(Glossary glossary, 
        IEnumerable<AuthenticationCredentialsProvider> credentials)
    {
        var getLanguagesRequest = new MatecatRequest(ApiEndpoints.Languages, Method.Get, credentials);
        var supportedLanguages =
            await new MatecatClient().ExecuteWithHandling<List<Language>>(getLanguagesRequest);

        var glossaryLanguageCodes = glossary.ConceptEntries
            .SelectMany(entry => entry.LanguageSections)
            .Select(section => section.LanguageCode)
            .Distinct();

        var targetGlossaryLanguageCodes = new List<string>();

        foreach (var code in glossaryLanguageCodes)
        {
            var targetLanguage = supportedLanguages.FirstOrDefault(language =>
                language.Code.Equals(code, StringComparison.OrdinalIgnoreCase));
            
            if (targetLanguage != null)
                targetGlossaryLanguageCodes.Add(targetLanguage.Code);
            else
            {
                var similarLanguages = supportedLanguages
                    .Where(language => language.Code.StartsWith(code, StringComparison.OrdinalIgnoreCase))
                    .OrderBy(language => language.Name.Length)
                    .ToArray();
                
                if (similarLanguages.Length > 0)
                    targetGlossaryLanguageCodes.Add(similarLanguages[0].Code);
            }
        }

        return targetGlossaryLanguageCodes.ToArray();
    }

    private static string[] GenerateLanguageHeaders(string[] languageCodes)
    {
        const int termLevelColumnsCountPerTerm = 3;

        var headers = new string[languageCodes.Length * termLevelColumnsCountPerTerm];
        headers[0] = Domain;
        headers[1] = Definition;

        for (var i = 0; i < languageCodes.Length; i++)
        {
            var index = i * termLevelColumnsCountPerTerm;

            headers[index] = languageCodes[i];
            headers[index + 1] = $"{Notes} {languageCodes[i]}";
            headers[index + 2] = $"{ExampleOfUse} {languageCodes[i]}";
        }

        return headers;
    }

    private static string? GetColumnValue(string columnName, GlossaryConceptEntry entry, string languageCode)
    {
        var languageSection = entry.LanguageSections.FirstOrDefault(ls =>
            ls.LanguageCode.Equals(languageCode, StringComparison.OrdinalIgnoreCase));

        if (languageSection != null)
        {
            switch (columnName)
            {
                case var name when name == languageCode:
                    return languageSection.Terms.First().Term;

                case var name when name == $"{Notes} {languageCode}":
                    return string.Join(';', languageSection.Terms.First().Notes ?? Enumerable.Empty<string>());

                case var name when name == $"{ExampleOfUse} {languageCode}":
                    return languageSection.Terms.First().UsageExample ?? string.Empty;

                default:
                    return null;
            }
        }

        if (columnName == languageCode || columnName == $"{Notes} {languageCode}" 
                                       || columnName == $"{ExampleOfUse} {languageCode}")
            return string.Empty;
        
        return null;
    }

    private static MemoryStream CreateExcelFile(List<List<string>> rows, string glossaryTitle)
    {
        static void InsertCell(Row row, string cellText, int index)
        {
            row.InsertAt(new Cell
            {
                DataType = CellValues.InlineString,
                InlineString = new InlineString { Text = new Text(cellText) }
            }, index);
        }
        
        var excelStream = new MemoryStream();
        using var spreadsheetDocument = SpreadsheetDocument.Create(excelStream, SpreadsheetDocumentType.Workbook);

        var workbookPart = spreadsheetDocument.AddWorkbookPart();
        workbookPart.Workbook = new Workbook();
        
        var worksheetPart = workbookPart.AddNewPart<WorksheetPart>();
        worksheetPart.Worksheet = new Worksheet();

        var headers = rows[0];
        var values = rows.Skip(1).ToList();
        
        var data = new SheetData();
        var rowId = 0;
        var row = new Row();
        
        for (var i = 0; i < headers.Count; i++)
        {
            InsertCell(row, headers[i], i);
        }
        
        data.InsertAt(row, rowId++);

        foreach (var value in values)
        {
            row = new Row();
            
            for (int i = 0; i < value.Count; i++)
            {
                InsertCell(row, value[i], i);
            }
            
            data.InsertAt(row, rowId++);
        }

        worksheetPart.Worksheet.Append(data);
        
        var sheets = spreadsheetDocument.WorkbookPart.Workbook.AppendChild(new Sheets());

        sheets.Append(new Sheet
        {
            Id = spreadsheetDocument.WorkbookPart.GetIdOfPart(worksheetPart),
            SheetId = 1,
            Name = glossaryTitle
        });
        
        workbookPart.Workbook.Save();

        spreadsheetDocument.Close();

        excelStream.Position = 0;
        return excelStream;
    }
    
    #endregion
    
    #endregion
}