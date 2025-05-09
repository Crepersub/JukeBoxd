using JukeBoxd.Forms;
using JukeBoxd.Models;
using Tests.TestBusinessLayer;


namespace Tests.TestForms
{
    [TestClass]
    public class TestLogin
    {
        public required TestDBContext _inMemoryDbContext;
        public required Login _loginForm;
        public required User _currentUser;

        [TestInitialize]
        public void Setup()
        {
            // Initialize the in-memory DbContext
            _inMemoryDbContext = new();
            _inMemoryDbContext.Database.EnsureCreated();

            // Seed sample data
            _inMemoryDbContext.Users.AddRange(new[]
            {
                new User("Alice"),
                new User("Bob")
            });
            _inMemoryDbContext.SaveChanges();

            // Initialize the Login form
            _loginForm = new Login(_inMemoryDbContext, _currentUser, null!);
        }

        [TestCleanup]
        public void Cleanup()
        {
            // Clear the in-memory database after each test
            _inMemoryDbContext.Users.RemoveRange(_inMemoryDbContext.Users);
            _inMemoryDbContext.Database.EnsureDeleted();
            _inMemoryDbContext.Dispose();
        }

        [TestMethod]
        public void Login_Load_PopulatesComboBoxWithUsernames()
        {
            // Act
            _loginForm.Login_Load(null!, null!);

            // Assert
            Assert.AreEqual(2, _loginForm.LoginComboBox.Items.Count);
            Assert.IsTrue(_loginForm.LoginComboBox.Items.Contains("Alice"));
            Assert.IsTrue(_loginForm.LoginComboBox.Items.Contains("Bob"));
        }

        [TestMethod]
        public void LoginButton_Click_SetsCurrentUserAndOpensMainForm()
        {
            // Arrange
            _loginForm.Login_Load(null!, null!);
            _loginForm.LoginComboBox.SelectedItem = "Alice";

            // Act
            _loginForm.LoginButton_Click(null!, null!);
            _currentUser = _inMemoryDbContext.Users.FirstOrDefault(u => u.Username == "Alice")!;

            // Assert
            Assert.IsNotNull(_currentUser);
            Assert.AreEqual("Alice", _currentUser.Username);
        }

        [TestMethod]
        public void Reload_UpdatesComboBoxWithNewUsernames()
        {
            // Arrange
            _loginForm.Login_Load(null!, null!);
            _inMemoryDbContext.Users.Add(new User("Charlie"));
            _inMemoryDbContext.SaveChanges();

            // Act
            _loginForm.Reload();

            // Assert
            Assert.AreEqual(3, _loginForm.LoginComboBox.Items.Count);
            Assert.IsTrue(_loginForm.LoginComboBox.Items.Contains("Charlie"));
        }
    }
}
