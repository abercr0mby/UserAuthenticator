using System;

namespace ConsoleApp1
{
    using UserAuthentication;
    using UserAuthentication.Store.Database;

    class Program
    {
        static void Main(string[] args)
        {
            var service = AuthenticationServiceFactory.GetAuthenticationServiceFile();
            service.Register("a@b.com", "pass");

            service = AuthenticationServiceFactory.GetAuthenticationServiceDatabase();
            service.Register("a@b.com", "pass");
        }
    }
}
