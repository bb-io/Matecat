using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Matecat.Models.Response.File;

[Display("Files")]
public record FilesResponse(List<FileReference> Files);