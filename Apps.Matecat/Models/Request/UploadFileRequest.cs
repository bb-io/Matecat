namespace Apps.Matecat.Models.Request;

public class UploadFileRequest
{
    public byte[] File { get; set; }
    public string FileName { get; set; }
}