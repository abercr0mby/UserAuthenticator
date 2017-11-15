namespace UserAuthentication
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    public sealed class AuthenticationService
    {
        internal AuthenticationService(AuthenticationDetailsStoreFile storeFile)
        {
            this.Store = storeFile;
        }

        internal AuthenticationService(AuthenticationDetailsStoreDatabase storeDB)
        {
            this.Store = storeDB;
        }

        private IAuthenticationDetailsStore Store { get; }

        public static string Md5Hash(string text)
        {
            var md5 = MD5.Create();

            var result = md5.ComputeHash(Encoding.ASCII.GetBytes(text));

            var stringBuilder = new StringBuilder();
            foreach (var t in result)
            {
                stringBuilder.Append(t.ToString("x2"));
            }

            return stringBuilder.ToString();
        }

        public AuthenticationDetails GetRegisteredDetails(string email)
        {
            return this.Store.GetByEmail(email);
        }

        public bool Login(string email, string password)
        {
            var user = this.Store.GetByEmail(email);

            if (user == null)
            {
                throw new Exception();
            }

            var hashedEmail = Md5Hash(email);

            if (hashedEmail == user.Email)
            {
                return true;
            }

            return false;
        }

        public void Register(string email, string password)
        {
            if (this.Store.GetByEmail(email) != null)
            {
                return;
            }

            this.Store.Insert(new AuthenticationDetails { Email = email, Password = Md5Hash(password) });
        }
    }
}
