using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Apps.Matecat.Models.Request
{
    public class UploadFilesRequest
    {
        [Display("Files", Description = "The file(s) to be uploaded. You may also upload your own translation memories (TMX). Files you send to the action should have English names, without any other language characters or special symbols")]
        public IEnumerable<FileReference> Files { get; set; }
    }
}
