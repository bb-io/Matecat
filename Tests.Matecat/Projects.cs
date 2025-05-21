using Apps.Matecat.Actions;
using Apps.Matecat.Connections;
using Apps.Matecat.Models.Request;
using Apps.Matecat.Models.Request.Project;
using Blackbird.Applications.Sdk.Common.Authentication;
using Blackbird.Applications.Sdk.Common.Files;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tests.Matecat.Base;

namespace Tests.Matecat;

[TestClass]
public class Projects : TestBase
{
    [TestMethod]
    public async Task Create_project_works()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        var file = new FileReference() { Name = "test.txt" };
        var result = await actions.CreateProject(
            new UploadFilesRequest { Files = new List<FileReference> { file } },
            new CreateProjectRequest { ProjectName = "Test project22", SourceLanguage = "en-US", TargetLanguages = new List<string>() { "nl-NL" } });

        string json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine("Returned project object:\n" + json);
        Assert.IsNotNull(result.Id);
    }

    [TestMethod]
    public async Task Get_project_works()
    {
        var actions = new ProjectActions(InvocationContext, FileManager);
        var result = await actions.GetProject("11561248/b9821bbca4a5");

        string json = JsonConvert.SerializeObject(result, Formatting.Indented);
        Console.WriteLine("Returned project object:\n" + json);
        Assert.IsNotNull(result.Id);
    }
}