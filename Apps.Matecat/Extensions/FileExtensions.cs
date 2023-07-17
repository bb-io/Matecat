using System.IO.Compression;
using Apps.Matecat.Models.Response;

namespace Apps.Matecat.Extensions;

public static class FileExtensions
{
    public static IEnumerable<FileEntity> GetFilesFromZip(this byte[] archive)
    {
        using var zipStream = new MemoryStream(archive);
        using var zip = new ZipArchive(zipStream, ZipArchiveMode.Read);

        foreach (var entry in zip.Entries)
        {
            using var entryStream = entry.Open();
            
            using var memoryStream = new MemoryStream();
            entryStream.CopyTo(memoryStream);
           
            yield return new(entry.Name, memoryStream.ToArray());
        }
    }
}