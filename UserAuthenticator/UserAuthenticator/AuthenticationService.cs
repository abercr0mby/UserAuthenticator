namespace UserAuthentication
{
    using System;
    using System.Security.Cryptography;
    using System.Text;
    using System.Text.RegularExpressions;
    using UserAuthentication.Core;

    public sealed class AuthenticationService
    {
        internal AuthenticationService(IAuthenticationDetailsStore storeFile)
        {
            this.Store = storeFile;
        }

        private IAuthenticationDetailsStore Store { get; }

        internal static string Md5Hash(string text)
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

            var hashedPassword = Md5Hash(password);

            if (hashedPassword == user.Password)
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
                throw new ArgumentException();
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
