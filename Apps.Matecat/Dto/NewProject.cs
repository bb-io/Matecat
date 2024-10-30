using Newtonsoft.Json;
using Apps.Matecat.Models.Request.Project;
using Apps.Matecat.Utils.Converter;

namespace Apps.Matecat.Dto
{
    public class NewProject
    {
        [JsonProperty("project_name")]
        public string ProjectName { get; set; }

        [JsonProperty("source_lang")]
        public string SourceLanguage { get; set; }

        [JsonProperty("target_lang")]
        public string TargetLanguage { get; set; }

        [JsonProperty("tms_engine")]
        public int? TmsEngine { get; set; }

        [JsonProperty("mt_engine")]
        public int? MtEngine { get; set; }

        [JsonProperty("private_tm_key")]
        public string? TmKey { get; set; }

        [JsonProperty("subject")]
        public string? Subject { get; set; }

        [JsonProperty("segmentation_rule")]
        public string? SegmentationRule { get; set; }

        [JsonProperty("due_date")]
        public string? DueDate { get; set; }

        [JsonProperty("id_team")]
        public string? IdTeam { get; set; }

        [JsonProperty("lexiqa")]
        public int? LexiQa { get; set; }

        [JsonProperty("speech2text")]
        public int? Speech2Text { get; set; }

        [JsonProperty("get_public_matches")]
        public bool? GetPublicMatches { get; set; }

        [JsonProperty("pretranslate_100")]
        public int? Pretranslate100 { get; set; }

        [JsonProperty("metadata")]
        public string? Metadata { get; set; }

        public NewProject(CreateProjectRequest request)
        {
            ProjectName = request.ProjectName;
            SourceLanguage = request.SourceLanguage;
            TargetLanguage = string.Join(',', request.TargetLanguages);
            TmsEngine = request.TmsEngine != null ? int.Parse(request.TmsEngine) : 1;
            MtEngine = request.MtEngine != null ? int.Parse(request.MtEngine) : 1;
            TmKey = request.TmKey != null ? string.Join(',', request.TmKey) : null;
            Subject = request.Subject;
            SegmentationRule = request.SegmentationRule;
            DueDate = request.DueDate.HasValue 
                ? new DateTimeOffset(request.DueDate.Value).ToUnixTimeSeconds().ToString() 
                : null;
            IdTeam = request.IdTeam;
            LexiQa = request.LexiQa.HasValue ? (request.LexiQa.Value ? 1 : 0) : 0;
            Speech2Text = request.Speech2Text.HasValue ? (request.Speech2Text.Value ? 1 : 0) : 0;
            GetPublicMatches = request.GetPublicMatches;
            Pretranslate100 = request.Pretranslate100.HasValue ? (request.Pretranslate100.Value ? 1 : 0) : 0;
            Metadata = request.Metadata;
        }
    }
}
