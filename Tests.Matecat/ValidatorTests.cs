using Apps.Matecat.Connections;
using Blackbird.Applications.Sdk.Common.Authentication;
using DocumentFormat.OpenXml.Drawing;
using Tests.Matecat.Base;

namespace Tests.Matecat;

[TestClass]
public class ConnectionValidatorTests : TestBase
{
    [TestMethod]
    public async Task ValidateConnection_ValidData_ShouldBeSuccessful()
    {
        var validator = new ConnectionValidator();

        var result = await validator.ValidateConnection(Creds, CancellationToken.None);

        Assert.IsTrue(result.IsValid);
    }

    [TestMethod]
    public async Task ValidateConnection_InvalidData_ShouldFail()
    {
        var validator = new ConnectionValidator();
        var newCredentials = Creds
            .Select(x => new AuthenticationCredentialsProvider(x.KeyName, x.Value + "_incorrect"));

        var result = await validator.ValidateConnection(newCredentials, CancellationToken.None);

        Assert.IsFalse(result.IsValid);
    }
}