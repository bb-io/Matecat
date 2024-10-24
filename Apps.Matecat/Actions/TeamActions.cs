using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request.Team;
using Apps.Matecat.Models.Response.Project;
using Apps.Matecat.Models.Response.Team;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Invocation;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class TeamActions : BaseInvocable
{
    #region Fields

    private readonly MatecatClient _client;

    private IEnumerable<AuthenticationCredentialsProvider> Creds =>
        InvocationContext.AuthenticationCredentialsProviders;

    #endregion

    #region Constructors

    public TeamActions(InvocationContext invocationContext) : base(invocationContext)
    {
        _client = new();
    }

    #endregion

    #region Actions

    #region Team actions

    // TODO: Evaluate if these are not really configuration endpoints
    //[Action("List teams", Description = "List all teams the current user is member of")]
    //public Task<AllTeamsResponse> ListTeams()
    //{
    //    var request = new MatecatRequest(ApiEndpoints.Teams, Method.Get, Creds);

    //    return _client.ExecuteWithHandling<AllTeamsResponse>(request);
    //}

    //[Action("Create team", Description = "Create a new team.")]
    //public Task<TeamResponse> CreateTeam([ActionParameter] CreateTeamRequest requestData)
    //{
    //    var request = new MatecatRequest(ApiEndpoints.Teams, Method.Post, Creds)
    //        .WithFormData(requestData);

    //    return _client.ExecuteWithHandling<TeamResponse>(request);
    //}
    
    //[Action("List team projects", Description = "List all projects of a team")]
    //public Task<AllProjectsResponse> ListTeamProjects([ActionParameter] TeamRequest team)
    //{
    //    var endpoint = $"{ApiEndpoints.Teams}/{team.TeamId}/projects";
    //    var request = new MatecatRequest(endpoint, Method.Get, Creds);

    //    return _client.ExecuteWithHandling<AllProjectsResponse>(request);
    //}

    #endregion

    #region Members actions

    //[Action("List team members", Description = "List all members of the specified team")]
    //public Task<MembersResponse> ListTeamMembers([ActionParameter] TeamRequest team)
    //{
    //    var endpoint = $"{ApiEndpoints.Teams}/{team.TeamId}/members";
    //    var request = new MatecatRequest(endpoint, Method.Get, Creds);

    //    return _client.ExecuteWithHandling<MembersResponse>(request);
    //}
    
    //[Action("Add members", Description = "Create new team memberships")]
    //public Task<MembersResponse> AddMembers(
    //    [ActionParameter] TeamRequest team,
    //    [ActionParameter] [Display("Members")] IEnumerable<string> members)
    //{
    //    var endpoint = $"{ApiEndpoints.Teams}/{team.TeamId}/members";
    //    var request = new MatecatRequest(endpoint, Method.Post, Creds)
    //    {
    //        AlwaysMultipartFormData = true
    //    };

    //    foreach (var (member, ind) in members.Select((val, ind) => (val, ind)))
    //        request.AddParameter($"members[{ind}]", member);

    //    return _client.ExecuteWithHandling<MembersResponse>(request);
    //}   
    
    //[Action("Remove member", Description = "Remove member from a team")]
    //public Task<MembersResponse> RemoveMember(
    //    [ActionParameter] TeamRequest team,
    //    [ActionParameter] [Display("User UID")] string memberId)
    //{
    //    var endpoint = $"{ApiEndpoints.Teams}/{team.TeamId}/members/{memberId}";
    //    var request = new MatecatRequest(endpoint, Method.Delete, Creds);
    
    //    return _client.ExecuteWithHandling<MembersResponse>(request);
    //}

    #endregion

    #endregion
}