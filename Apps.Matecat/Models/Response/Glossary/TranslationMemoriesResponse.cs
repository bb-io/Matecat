using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Glossary;

public class TranslationMemoriesResponse
{
    [JsonProperty("private_keys")]
    public IEnumerable<TranslationMemoryItem>? PrivateKeys { get; set; }
    
    [JsonProperty("shared_keys")]
    public IEnumerable<TranslationMemoryItem>? SharedKeys { get; set; }
}

public record TranslationMemoryItem(string Key, string Name);