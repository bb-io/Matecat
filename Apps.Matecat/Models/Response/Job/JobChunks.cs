using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class JobChunks
{
    [Display("Job ID"), JsonProperty("id")]
    public string JobId { get; set; } = string.Empty;
    
    [Display("Chunks")]
    public List<Job> Chunks { get; set; } = new();

    [Display("First chunk")] 
    public Job FirstChunk => Chunks.FirstOrDefault() ?? new();
    
    [Display("Chunk count")]
    public int ChunkCount => Chunks.Count;
}