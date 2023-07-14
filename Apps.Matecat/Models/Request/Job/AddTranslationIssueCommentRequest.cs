using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Request.Job;

public class AddTranslationIssueCommentRequest : TranslationIssueRequest
{
    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }

    [JsonProperty("id_qa_entry")]
    [Display("QA Entry ID")]
    public string IdQaEntry { get; set; }

    [JsonProperty("source_page")]
    [Display("Source Page")]
    public string SourcePage { get; set; }

    [JsonProperty("uid")]
    [Display("Uid")]
    public string Uid { get; set; }
}