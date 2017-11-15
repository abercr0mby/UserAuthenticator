namespace UserAuthentication.Unit.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class AuthenticationServiceTests
    {
        private static AuthenticationDetails details;

        private static AuthenticationService service;

        [ClassInitialize]
        public static void SetUp(TestContext context)
        {
            service = new AuthenticationService();
            details = new AuthenticationDetails { Email = "a@b.com", Password = "let-me-in" };
        }

        [TestMethod]
        public void AuthenticationService_RegisterRegistersDetails()
        {
            var a = "12qwdqwd";
            // Arrange
            // Act
            service.Register(details.Email, details.Password);
            var registeredDetails = service.GetRegisteredDetails(details.Email);

            // Assert
            Assert.AreEqual(details.Email, registeredDetails.Email);
        }
    }
}
