using NUnit.Framework;

namespace SocialNetwork.Tests
{
    [TestFixture]
    public class UserTests
    {
        [Test]
        public void UserRegistration_MustReturnNullException()
        {
            SocialNetwork.BLL.Services.UserService userService = new BLL.Services.UserService();
            Assert.Throws<ArgumentNullException>(() =>
                userService.Register(new BLL.Models.UserRegistrationData()));
        }

        [Test]
        public void UserAuthentication_MustReturnTrue()
        {
            SocialNetwork.BLL.Services.UserService userService = new BLL.Services.UserService();
            var userAuthenticationData = new BLL.Models.UserAuthenticationData()
            {
                Email = "1@1.ru",
                Password = "12345678"
            };
            var user = userService.Authenticate(userAuthenticationData);
            Assert.True(user != null);
        }
    }
}