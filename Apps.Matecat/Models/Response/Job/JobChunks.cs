using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat.Models.Response.Job;

public class JobChunks
{
    [Display("Job ID")]
    public string JobId { get; set; } = string.Empty;
    
    [Display("Chunks")]
    public List<Job> Chunks { get; set; } = new();

    [Display("First chunk")] 
    public Job FirstChunk => Chunks.FirstOrDefault() ?? new();
    
    [Display("Chunk count")]
    public int ChunkCount => Chunks.Count;
}