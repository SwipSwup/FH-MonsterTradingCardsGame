using Server.Utilities;

namespace Tests.Server.Utilities;

[TestFixture]
public class PasswordUtilitiesTests
{
    private const string Password = "testpassword";

    [Test]
    public void HashPassword_ShouldReturnValidHash()
    {
        string hashedPassword = PasswordUtilities.HashPassword(Password);

        Assert.IsNotEmpty(hashedPassword);
        Assert.That(hashedPassword, Is.Not.EqualTo(Password));
    }

    [Test]
    public void VerifyPassword_ShouldReturnTrueForCorrectPassword()
    {
        string hashedPassword = PasswordUtilities.HashPassword(Password);

        bool result = PasswordUtilities.VerifyPassword(Password, hashedPassword);

        Assert.IsTrue(result);
    }

    [Test]
    public void VerifyPassword_ShouldReturnFalseForIncorrectPassword()
    {
        string hashedPassword = PasswordUtilities.HashPassword(Password);

        bool result = PasswordUtilities.VerifyPassword("incorrectPassword", hashedPassword);

        Assert.IsFalse(result);
    }
}