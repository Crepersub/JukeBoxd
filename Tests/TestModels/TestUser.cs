using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using JukeBoxd.Models;

namespace Tests.TestModels
{
    [TestClass]
    public class TestUser
    {
        [TestMethod]
        public void User_Constructor_ShouldInitializeUsername()
        {
            // Arrange
            string expectedUsername = "TestUser";

            // Act
            User user = new User(expectedUsername);

            // Assert
            Assert.AreEqual(expectedUsername, user.Username);
        }

        [TestMethod]
        public void User_Entries_ShouldBeInitializedAsEmptyList()
        {
            // Arrange
            User user = new User("TestUser");

            // Act
            List<Entry> entries = user.Entries;

            // Assert
            Assert.IsNotNull(entries);
            Assert.AreEqual(0, entries.Count);
        }

        [TestMethod]
        public void User_Id_ShouldBeSetAndRetrievedCorrectly()
        {
            // Arrange
            User user = new User("TestUser");
            int expectedId = 42;

            // Act
            user.Id = expectedId;

            // Assert
            Assert.AreEqual(expectedId, user.Id);
        }
    }
}
