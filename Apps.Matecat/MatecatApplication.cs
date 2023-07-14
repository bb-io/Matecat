using Blackbird.Applications.Sdk.Common;

namespace Apps.Matecat;

public class MatecatApplication : IApplication
{
    public string Name
    {
        get => "Matecat";
        set { }
    }

    public T GetInstance<T>()
    {
        throw new NotImplementedException();
    }
}