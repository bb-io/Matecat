using Apps.Matecat.Constants;
using Blackbird.Applications.Sdk.Utils.Sdk.DataSourceHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Matecat.DataSourceHandlers.EnumDataHandlers
{
    public class DerivedStatusDataHandler : EnumDataHandler
    {
        protected override Dictionary<string, string> EnumValues => new()
        {
            {JobStatus.New, "New"},
            {JobStatus.InTranslation, "In translation"},
            {JobStatus.Translated, "Translated"},
            {JobStatus.InRevision, "In revision"},
            {JobStatus.Revised, "Revised"},
        };
    }
}
