using Apps.Matecat.DataSourceHandlers.EnumDataHandlers;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Dictionaries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Matecat.Polling.Models
{
    public class OptionalDerivedStatus
    {
        [Display("New status")]
        [StaticDataSource(typeof(DerivedStatusDataHandler))]
        public string? DerivedStatus { get; set; }
    }
}
