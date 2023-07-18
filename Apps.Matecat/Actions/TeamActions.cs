using Apps.Matecat.Constants;
using Apps.Matecat.Extensions;
using Apps.Matecat.Models.Request.Team;
using Apps.Matecat.Models.Response.Project;
using Apps.Matecat.Models.Response.Team;
using Apps.Matecat.RestSharp;
using Blackbird.Applications.Sdk.Common;
using Blackbird.Applications.Sdk.Common.Actions;
using Blackbird.Applications.Sdk.Common.Authentication;
using RestSharp;

namespace Apps.Matecat.Actions;

[ActionList]
public class TeamActions
{
    #region Fields

    private readonly MatecatClient _client;

    #endregion

    #region Constructors

    public TeamActions()
    {
        _client = new();
    }

    #endregion

    #region Actions

    #region Team actions

    [Action("List teams", Description = "List all teams the current user is member of")]
    public Task<AllTeamsResponse> ListTeams(IEnumerable<AuthenticationCredentialsProvider> creds)
    {
        var request = new MatecatRequest(ApiEndpoints.Teams, Method.Get, creds);

        return _client.ExecuteWithHandling<AllTeamsResponse>(request);
    }

    [Action("Create team", Description = "Create a new team.")]
    public Task<TeamResponse> CreateTeam(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] CreateTeamRequest requestData)
    {
        var request = new MatecatRequest(ApiEndpoints.Teams, Method.Post, creds)
            .WithFormData(requestData);

        return _client.ExecuteWithHandling<TeamResponse>(request);
    }
    
    [Action("List team projects", Description = "List all projects of a team")]
    public Task<AllProjectsResponse> ListTeamProjects(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Team ID")] int teamId)
    {
        var endpoint = $"{ApiEndpoints.Teams}/{teamId}/projects";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        return _client.ExecuteWithHandling<AllProjectsResponse>(request);
    }

    #endregion

    #region Members actions

    [Action("List team members", Description = "List all members of the specified team")]
    public Task<MembersResponse> ListTeamMembers(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Team ID")] int teamId)
    {
        var endpoint = $"{ApiEndpoints.Teams}/{teamId}/members";
        var request = new MatecatRequest(endpoint, Method.Get, creds);

        return _client.ExecuteWithHandling<MembersResponse>(request);
    }
    
    [Action("Add members", Description = "Create new team memberships")]
    public Task<MembersResponse> AddMembers(IEnumerable<AuthenticationCredentialsProvider> creds,
        [ActionParameter] [Display("Team ID")] int teamId,
        [ActionParameter] [Display("Members")] IEnumerable<string> members)
    {
        var endpoint = $"{ApiEndpoints.Teams}/{teamId}/members";
        var request = new MatecatRequest(endpoint, Method.Post, creds)
        {
            AlwaysMultipartFormData = true
        };

        foreach (var (member, ind) in members.Select((val, ind) => (val, ind)))
            request.AddParameter($"members[{ind}]", member);

        return _client.ExecuteWithHandling<MembersResponse>(request);
    }   
    
    // [Action("Remove member", Description = "Remove member from a team")]
    // public Task<MembersResponse> RemoveMember(IEnumerable<AuthenticationCredentialsProvider> creds,
    //     [ActionParameter] [Display("Team ID")] int teamId,
    //     [ActionParameter] [Display("Member ID")] int memberId)
    // {
    //     var endpoint = $"{ApiEndpoints.Teams}/{teamId}/members/{memberId}";
    //     var request = new MatecatRequest(endpoint, Method.Delete, creds);
    //
    //     return _client.ExecuteWithHandling<MembersResponse>(request);
    // }

    #endregion

    #endregion
}