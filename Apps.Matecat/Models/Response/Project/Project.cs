using Apps.Matecat.Constants;
using Apps.Matecat.Models.Response.Job;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Project;

public class Project
{
    [Display("Project ID")]
    [JsonProperty("ID")] 
    public string Id { get; set; }

    [Display("Project password")]
    [JsonProperty("password")] 
    public string Password { get; set; }

    [Display("Project ID and password")] 
    public string IdAndPassword => $"{Id}/{Password}";

    [Display("Name")]
    [JsonProperty("name")] 
    public string Name { get; set; }

    [JsonProperty("id_team")]
    [Display("Team ID")]
    public string IdTeam { get; set; }

    [JsonProperty("id_assignee")]
    [Display("Assignee ID")]
    public string IdAssignee { get; set; }

    [JsonProperty("analysis")]
    public Analysis Analysis { get; set; }

    [JsonProperty("create_date")]
    [Display("Creation date")]
    public DateTime CreateDate { get; set; }

    [JsonProperty("fast_analysis_wc")]
    [Display("Fast analysis word count")]
    public int FastAnalysisWc { get; set; }

    [JsonProperty("standard_analysis_wc")]
    [Display("Standard analysis word count")]
    public int StandardAnalysisWc { get; set; }

    [JsonProperty("project_slug")]
    [Display("Project slug")]
    public string ProjectSlug { get; set; }

    [JsonProperty("features")]
    [Display("Features")]
    public string Features { get; set; }

    [JsonProperty("is_cancelled")]
    [Display("Is cancelled")]
    public bool IsCancalled { get; set; }

    [JsonProperty("is_archived")]
    [Display("Is archived")]
    public bool IsArchived { get; set; }

    [JsonProperty("remote_file_service")]
    [Display("Remote file service")]
    public string? RemoteFileService { get; set; }

    [JsonProperty("due_date")]
    [Display("Due date")]
    public DateTime? DueDate { get; set; }

    [JsonProperty("jobs")] 
    public List<Job.Job> Jobs { get; set; }

    [Display("Derived status")]
    public string DerivedStatus
    {
        get
        {
            var statusses = new List<string> { JobStatus.New, JobStatus.InTranslation, JobStatus.Translated, JobStatus.InRevision, JobStatus.Revised };
            foreach(var status in statusses)
            {
                if (Jobs.Any(x => x.DerivedStatus == status)) return status;
            }
            return JobStatus.New;
        }
    }
}

public class Analysis
{
    [Display("Name")]
    [JsonProperty("name")]
    public string Name { get; set; }

    [Display("Status")]
    [JsonProperty("status")]
    public string Status { get; set; }

    [JsonProperty("create_date")]
    [Display("Creation date")]
    public DateTime CreateDate { get; set; }

    [Display("Subject")]
    [JsonProperty("subject")]
    public string Subject { get; set; }

    [Display("Analysis URL")]
    [JsonProperty("analyze_url")]
    public string AnalyzeUrl { get; set; }

    [JsonProperty("jobs")]
    [Display("Analysis jobs")]
    public IEnumerable<AnalysisJob> Jobs { get; set; }

    [JsonProperty("summary")]
    [Display("Summary")]
    public AnalysisSummary Summary { get; set; }
}

public class AnalysisSummary
{
    [JsonProperty("in_queue_before")]
    [Display("In queue before")]
    public int InQueueBefore { get; set; }

    [JsonProperty("total_segments")]
    [Display("Total segments")]
    public int TotalSegments { get; set; }

    [JsonProperty("segments_analyzed")]
    [Display("Segments analyzed")]
    public int SegmentsAnalyzed { get; set; }

    [JsonProperty("status")]
    [Display("Status")]
    public string Status { get; set; }

    [JsonProperty("total_raw")]
    [Display("Total word count")]
    public int TotalRaw { get; set; }

    [JsonProperty("total_industry")]
    [Display("Industry weighted")]
    public int TotalIndustry { get; set; }

    [JsonProperty("total_equivalent")]
    [Display("Matecat weighted")]
    public int TotalEquivalent { get; set; }

    [JsonProperty("discount")]
    [Display("Discount")]
    public int Discount { get; set; }
}

public class AnalysisJob
{
    [JsonProperty("ID")]
    [Display("Job ID")]
    public string Id { get; set; }

    [JsonProperty("source")]
    [Display("Source code")]
    public string SourceCode { get; set; }

    [JsonProperty("source_name")]
    [Display("Source name")]
    public string SourceName { get; set; }

    [JsonProperty("target")]
    [Display("Target code")]
    public string TargetCode { get; set; }

    [JsonProperty("target_name")]
    [Display("Target name")]
    public string TargetName { get; set; }

    [JsonProperty("total_raw")]
    [Display("Total word count")]
    public int TotalRaw { get; set; }

    [JsonProperty("total_industry")]
    [Display("Industry weighted")]
    public int TotalIndustry { get; set; }

    [JsonProperty("total_equivalent")]
    [Display("Matecat weighted")]
    public int TotalEquivalent { get; set; }

    [JsonProperty("count_unit")]
    [Display("Count unit")]
    public string CountUnit { get; set; }
}