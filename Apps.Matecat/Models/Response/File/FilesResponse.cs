using Blackbird.Applications.Sdk.Common.Files;

namespace Apps.Matecat.Models.Response.File;

public record FilesResponse(List<FileReference> Files);