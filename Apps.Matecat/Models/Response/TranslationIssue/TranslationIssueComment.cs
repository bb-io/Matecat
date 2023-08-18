using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.TranslationIssue;

public class TranslationIssueComment
{
    [JsonProperty("id")]
    [Display("ID")]
    public string Id { get; set; }

    [JsonProperty("uid")]
    [Display("UID")]
    public string Uid { get; set; }

    [JsonProperty("id_qa_entry")]
    [Display("QA entry ID")]
    public string QaEntryId { get; set; }

    [JsonProperty("create_date")]
    [Display("Create date")]
    public DateTime CreateDate { get; set; }

    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }

    [JsonProperty("source_page")]
    [Display("Source page")]
    public int? SourcePage { get; set; }
}