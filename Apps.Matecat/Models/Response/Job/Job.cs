using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class Job
{
    [JsonProperty("id")] 
    [Display("Job ID")] 
    public string Id { get; set; }

    [JsonProperty("password")]
    [Display("Job password")]
    public string Password { get; set; }

    [Display("Job ID and password")] 
    public string IdAndPassword => $"{Id}/{Password}";

    [JsonProperty("source")]
    [Display("Source code")]
    public string SourceCode { get; set; }

    [JsonProperty("target")]
    [Display("Target code")]
    public string TargetCode { get; set; }

    [JsonProperty("sourceTxt")]
    [Display("Source name")]
    public string SourceTxt { get; set; }

    [JsonProperty("targetTxt")]
    [Display("Target name")]
    public string TargetTxt { get; set; }

    [JsonProperty("job_first_segment")]
    [Display("First segment ID")]
    public string JobFirstSegment { get; set; }

    [JsonProperty("status")]
    [Display("Status")]
    public string Status { get; set; }

    [JsonProperty("subject")]
    [Display("Subject")]
    public string Subject { get; set; }

    [JsonProperty("owner")]
    [Display("Owner")]
    public string Owner { get; set; }

    [JsonProperty("open_threads_count")]
    [Display("Open threads count")]
    public int OpenThreadsCount { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("formatted_create_date")]
    [Display("Formatted create date")]
    public string FormattedCreateDate { get; set; }

    [JsonProperty("quality_overall")]
    [Display("Overall quality")]
    public string QualityOverall { get; set; }

    [JsonProperty("pee")] 
    [Display("Pee")] 
    public int Pee { get; set; }

    [JsonProperty("tte")] 
    [Display("Tte")] 
    public int Tte { get; set; }

    [JsonProperty("private_tm_key")]
    [Display("Private TM keys")]
    public IEnumerable<string> PrivateTmKeys { get; set; }

    [JsonProperty("translator")]
    public Translator Translator { get; set; }

    [JsonProperty("urls")]
    public Urls Urls { get; set; }

    [JsonProperty("total_raw_wc")]
    [Display("Total raw word count")]
    public int TotalRawWc { get; set; }

    [JsonProperty("standard_wc")]
    [Display("Standard word count")]
    public int StandardWc { get; set; }
}

public class Urls
{
    [JsonProperty("translate_url")]
    [Display("Transalte URL")]
    public string TranslateUrl { get; set; }

    [JsonProperty("original_download_url")]
    [Display("Original download URL")]
    public string OriginalDownloadUrl { get; set; }

    [JsonProperty("translation_download_url")]
    [Display("Translation download URL")]
    public string TranslationDownloadUrl { get; set; }

    [JsonProperty("xliff_download_url")]
    [Display("XLIFF download URL")]
    public string XliffDownloadUrl { get; set; }
}