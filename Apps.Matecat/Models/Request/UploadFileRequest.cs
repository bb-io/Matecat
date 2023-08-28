using Blackbird.Applications.Sdk.Common;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Matecat.Models.Request;

public class UploadFileRequest
{
    public File File { get; set; }
    
    [Display("File name")]
    public string? FileName { get; set; }
}