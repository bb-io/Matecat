namespace Apps.Matecat.Models.Request.Team;

public class CreateTeamRequest
{
    public string Name { get; set; }
    public string Type { get; set; }
    public IEnumerable<string> Members { get; set; }
}