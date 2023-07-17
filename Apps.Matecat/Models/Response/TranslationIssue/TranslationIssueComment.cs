using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.TranslationIssue;

public class TranslationIssueComment
{
    [JsonProperty("id")]
    [Display("ID")]
    public long Id { get; set; }

    [JsonProperty("uid")]
    [Display("UID")]
    public long Uid { get; set; }

    [JsonProperty("id_qa_entry")]
    [Display("QA entry ID")]
    public long QaEntryId { get; set; }

    [JsonProperty("create_date")]
    [Display("Create date")]
    public DateTime CreateDate { get; set; }

    [JsonProperty("comment")]
    [Display("Comment")]
    public string Comment { get; set; }

    [JsonProperty("source_page")]
    [Display("Source page")]
    public int SourcePage { get; set; }
}