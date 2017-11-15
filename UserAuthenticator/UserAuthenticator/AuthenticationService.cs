namespace UserAuthentication
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using UserAuthentication.Core;
    using UserAuthentication.Store.File;

#if NET461
    using UserAuthentication.Store.Database;
#endif

    public sealed class AuthenticationService
    {
        internal AuthenticationService(AuthenticationDetailsStoreFile storeFile)
        {
            this.Store = storeFile;
        }

#if NET461
        internal AuthenticationService(AuthenticationDetailsStoreDatabase storeDb)
        {
            this.Store = storeDb;
        }
#endif

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
                return false;
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
            if (!this.IsEmailValid(email))
            {
                throw new ArgumentException();
            }

            if (this.Store.GetByEmail(email) != null)
            {
                return;
            }

            this.Store.Insert(new AuthenticationDetails { Email = email, Password = Md5Hash(password) });
        }

        public bool IsEmailValid(string email)
        {
            if (string.IsNullOrEmpty(email))
            {
                return false;
            }

            try
            {
                return Regex.IsMatch(email, @"^(?("")("".+?(?<!\\)""@)|(([0-9a-z]((\.(?!\.))|[-!#\$%&'\*\+/=\?\^`\{\}\|~\w])*)(?<=[0-9a-z])@))" + @"(?(\[)(\[(\d{1,3}\.){3}\d{1,3}\])|(([0-9a-z][-\w]*[0-9a-z]*\.)+[a-z0-9][\-a-z0-9]{0,22}[a-z0-9]))$", RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250));
            }
            catch (RegexMatchTimeoutException)
            {
                return false;
            }
        }
    }
}
