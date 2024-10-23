using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Matecat.Constants
{
    public class JobStatus
    {
        public const string New = "NEW"; // New = total
        public const string InTranslation = "IN_TRANSLATION"; // Translated != 0 && New != 0
        public const string Translated = "TRANSLATED"; // Translated = total
        public const string InRevision = "IN_REVISION"; // Translated != 0 && Approved != 0
        public const string Revised = "REVISED"; // Approved = total
    }
}
