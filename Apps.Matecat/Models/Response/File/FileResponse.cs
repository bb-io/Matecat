using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Matecat.Models.Response.File;

[Display("File")]
public record FileResponse(FileReference File);