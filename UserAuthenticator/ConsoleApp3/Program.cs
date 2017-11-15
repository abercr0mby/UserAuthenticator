namespace ConsoleApp3
{
    using UserAuthentication;

    internal class Program
    {
        private static void Main(string[] args)
        {
            var service = AuthenticationServiceFactory.GetAuthenticationServiceFile();
            service.Register("a@b.com", "pass");

            service = AuthenticationServiceFactory.GetAuthenticationServiceDatabase();
            service.Register("a@b.com", "pass");
        }
    }
}
