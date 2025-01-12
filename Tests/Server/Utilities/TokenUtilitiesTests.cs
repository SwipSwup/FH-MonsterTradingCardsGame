using System.Security.Claims;
using Server.Utilities;
using Shared.DTOs;

namespace Tests.Server.Utilities
{
    [TestFixture]
    public class TokenUtilitiesTests
    {
        private UserDto _user;

        [SetUp]
        public void SetUp()
        {
            _user = new UserDto
            {
                UserId = 1,
                Username = "testuser"
            };
        }

        [Test]
        public void GenerateJwtToken_ShouldReturnValidToken()
        {
            string token = TokenUtilities.GenerateJwtToken(_user);

            Assert.IsNotEmpty(token);
        }

        [Test]
        public void ValidateToken_ShouldReturnTrueForValidToken()
        {
            string token = TokenUtilities.GenerateJwtToken(_user);

            bool isValid = TokenUtilities.ValidateToken(token, out ClaimsPrincipal? principal);

            Assert.IsTrue(isValid);
            Assert.IsNotNull(principal);
            Assert.That(_user.Username, Is.EqualTo(principal?.Identity?.Name));
        }

        [Test]
        public void ValidateToken_ShouldReturnFalseForInvalidToken()
        {
            bool isValid = TokenUtilities.ValidateToken("InvalidToken", out ClaimsPrincipal? principal);

            Assert.IsFalse(isValid);
            Assert.IsNull(principal);
        }
    }
}