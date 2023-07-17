using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Response.TranslationIssue;

public record TranslationIssuesResponse([property: Display("Translation issues")]
    List<TranslationIssue> TranslationIssues);