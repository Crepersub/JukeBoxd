using JukeBoxd.Models;
namespace Tests.TestModels;


[TestClass]
public sealed class TestClientSecret
{
    [TestMethod]
    public void TestClientIDProperty()
    {
        // Arrange
        var clientSecret = new ClientSecret();
        var expectedClientID = "12345";

        // Act
        clientSecret.ClientID = expectedClientID;

        // Assert
        Assert.AreEqual(expectedClientID, clientSecret.ClientID);
    }

    [TestMethod]
    public void TestClientSecretProperty()
    {
        // Arrange
        var clientSecret = new ClientSecret();
        var expectedSecret = "mySecretKey";

        // Act
        clientSecret.clientSecret = expectedSecret;

        // Assert
        Assert.AreEqual(expectedSecret, clientSecret.clientSecret);
    }

    [TestMethod]
    public void TestDefaultValues()
    {
        // Arrange & Act
        var clientSecret = new ClientSecret();

        // Assert
        Assert.IsNull(clientSecret.ClientID);
        Assert.IsNull(clientSecret.clientSecret);
    }
}
