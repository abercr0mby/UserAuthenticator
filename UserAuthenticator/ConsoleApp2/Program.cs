using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2
{
    using UserAuthentication;

    class Program
    {
        static void Main(string[] args)
        {
            var service = AuthenticationServiceFactory.GetAuthenticationServiceFile();
            service.Register("a@b.com", "pass");
        }
    }
}
