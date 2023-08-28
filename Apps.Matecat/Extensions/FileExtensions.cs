using System.IO.Compression;
using System.Net.Mime;
using Apps.Matecat.Models.Response;
using File = Blackbird.Applications.Sdk.Common.Files.File;

namespace Apps.Matecat.Extensions;

public static class FileExtensions
{
    public static IEnumerable<File> GetFilesFromZip(this byte[] archive)
    {
        using var zipStream = new MemoryStream(archive);
        using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            using var entryStream = entry.Open();
            
            using var memoryStream = new MemoryStream();
            entryStream.CopyTo(memoryStream);
           
            yield return new(memoryStream.ToArray())
            {
                Name = entry.Name,
                ContentType = MediaTypeNames.Application.Octet
            };
        }
    }
}