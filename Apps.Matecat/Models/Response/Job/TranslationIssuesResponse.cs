using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Response.Job;

public record TranslationIssuesResponse([property: Display("Translation issues")]
    List<TranslationIssue> TranslationIssues);