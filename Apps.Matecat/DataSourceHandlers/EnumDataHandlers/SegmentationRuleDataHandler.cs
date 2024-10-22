using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers
{
    public class SegmentationRuleDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {"", "General"},
            {"patent", "Patent"}
        };
    }
}
