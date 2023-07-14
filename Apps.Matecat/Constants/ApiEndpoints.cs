namespace Apps.Matecat.Constants;

public class ApiEndpoints
{
    #region Versions

    public const string V1 = "/v1";
    public const string V2 = "/v2";

    #endregion

    #region Projects

    public const string NewProject = V1 + "/new";
    public const string Projects = V2 + "/projects";

    #endregion

    #region Jobs

    public const string Jobs = V2 + "/jobs";
    public const string Translation = V2 + "/translation";

    #endregion
}