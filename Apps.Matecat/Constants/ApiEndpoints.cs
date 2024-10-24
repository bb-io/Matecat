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

    #region TranslationMemories

    public const string TranslationMemories = V2 + "/keys/list";
    public const string ImportGlossary = V2 + "/glossaries/import/";

    #endregion

    #region Other

    public const string Engines = V2 + "/engines/list";
    public const string PayableRates = V2 + "/payable_rate";

    #endregion
}