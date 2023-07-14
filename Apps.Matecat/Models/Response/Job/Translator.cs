using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class Translator
{
    [JsonProperty("email")]
    [Display("Email")]
    public string Email { get; set; }

    [JsonProperty("added_by")]
    [Display("Added by")]
    public int AddedBy { get; set; }

    [JsonProperty("delivery_date")]
    [Display("Delivery date")]
    public string DeliveryDate { get; set; }

    [JsonProperty("delivery_timestamp")]
    [Display("Delivery timestamp")]
    public string DeliveryTimestamp { get; set; }

    [JsonProperty("source")]
    [Display("Source")]
    public string Source { get; set; }

    [JsonProperty("target")]
    [Display("Target")]
    public string Target { get; set; }

    [JsonProperty("id_translator_profile")]
    [Display("Translator profile ID")]
    public int TranslatorProfileId { get; set; }
}