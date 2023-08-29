using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class Job
{
    [JsonProperty("ID")] [Display("Job")] public string Id { get; set; }

    [JsonProperty("password")]
    [Display("Password")]
    public string Password { get; set; }
    
    [JsonProperty("revise_passwords")]
    [Display("Revise passwords")]
    public IEnumerable<RevisePassword> RevisePasswords { get; set; }

    [Display("ID and password")] public string IdAndPassword => $"{Id}/{Password}";

    [JsonProperty("source")]
    [Display("Source")]
    public string Source { get; set; }

    [JsonProperty("target")]
    [Display("Target")]
    public string Target { get; set; }

    [JsonProperty("sourceTxt")]
    [Display("Source Text")]
    public string SourceTxt { get; set; }

    [JsonProperty("targetTxt")]
    [Display("Target Text")]
    public string TargetTxt { get; set; }

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

    [JsonProperty("create_timestamp")]
    [Display("Create timestamp")]
    public int CreateTimestamp { get; set; }

    [JsonProperty("created_at")]
    [Display("Created at")]
    public DateTime CreatedAt { get; set; }

    [JsonProperty("create_date")]
    [Display("Create date")]
    public DateTime CreateDate { get; set; }

    [JsonProperty("formatted_create_date")]
    [Display("Formatted create date")]
    public string FormattedCreateDate { get; set; }

    [JsonProperty("quality_overall")]
    [Display("Overall quality")]
    public string QualityOverall { get; set; }

    [JsonProperty("pee")] [Display("Pee")] public int Pee { get; set; }

    [JsonProperty("tte")] [Display("Tte")] public int Tte { get; set; }

    public Translator Translator { get; set; }
}