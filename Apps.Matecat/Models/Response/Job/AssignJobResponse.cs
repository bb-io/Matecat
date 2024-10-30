using Apps.Matecat.Utils.Converter;
using Blackbird.Applications.Sdk.Common;
using Newtonsoft.Json;

namespace Apps.Matecat.Models.Response.Job;

public class AssignJobResponse
{
    public TranslatorJobResponse Job { get; set; } = new();
}

public class TranslatorJobResponse
{
    [Display("Job ID"), JsonProperty("id")]
    public string Id { get; set; } = string.Empty;
    
    [Display("Job password"), JsonProperty("password")]
    public string Password { get; set; } = string.Empty;
    
    [Display("Translator"), JsonProperty("translator")]
    public TranslatorDto Translator { get; set; } = new();
}

public class TranslatorDto
{
    [Display("Translator email"), JsonProperty("email")]
    public string Email { get; set; } = string.Empty;
    
    [Display("Added by"), JsonProperty("added_by")]
    public int AddedBy { get; set; }
    
    [Display("Delivery date"), JsonProperty("delivery_date_fake")]
    public DateTime DeliveryDate { get; set; }
    
    [Display("Delivery date (string)"), JsonProperty("delivery_date")]
    public string DeliveryDateString { get; set; } = string.Empty;
    
    [DefinitionIgnore, JsonProperty("delivery_timestamp"), JsonConverter(typeof(UnixDateTimeConverter))]
    public DateTime DeliveryTimestamp { get; set; }
    
    [Display("Source language"), JsonProperty("source")]
    public string Source { get; set; } = string.Empty;
    
    [Display("Target language"), JsonProperty("target")]
    public string Target { get; set; } = string.Empty;
    
    public void SetDeliveryDate()
    {
        DeliveryDate = DeliveryTimestamp;
    }
}
