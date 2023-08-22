namespace Apps.Matecat.Constants;

public class ApiEndpoints
{
    #region Versions

    public const string V1 = "/api/v1";
    public const string V2 = "/api/v2";

    #endregion

    #region Projects

    public const string NewProject = V1 + "/new";
    public const string Projects = V2 + "/projects";

    #endregion

    #region Jobs

    public const string Jobs = V2 + "/jobs";
    public const string Translation = "/translation";
    public const string Tmx = "/TMX";

    #endregion

    #region Teams

    public const string Teams = V2 + "/teams";

    #endregion

    #region Languages

    public const string Languages = V2 + "/languages";


    #endregion
}