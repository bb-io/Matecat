using RestSharp;

namespace Apps.Matecat.Extensions;

public static class RestRequestExtensions
{
    public static RestRequest WithFormData(
        this RestRequest request,
        object parameters,
        bool isMultipartFormData = false)
    {
        parameters
            .AsDictionary()
            .ToList()
            .ForEach(x => request.AddParameter(x.Key, x.Value));

        request.AlwaysMultipartFormData = isMultipartFormData;
        return request;
    }

    public static RestRequest WithFile(this RestRequest request, byte[] file, string fileName)
        => request.AddFile($"files[{fileName}]", file, fileName);
}