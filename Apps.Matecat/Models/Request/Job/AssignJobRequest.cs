using Apps.Matecat.DataSourceHandlers.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;

namespace Apps.Matecat.Models.Request.Job;

public class AssignJobRequest : JobRequest
{
    [Display("Translator email")]
    public string Email { get; set; } = string.Empty;

    [Display("Delivery date")]
    public DateTime DeliveryDate { get; set; }

    [Display("Time zone", Description = "Time zone to convert the Delivery date parameter expressed as offset based on UTC. Example 1.0, -7.0 etc."), StaticDataSource(typeof(TimeZoneDataSource))]
    public string Timezone { get; set; } = string.Empty;
}