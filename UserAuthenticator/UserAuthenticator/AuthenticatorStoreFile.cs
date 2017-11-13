namespace UserAuthenticator
{
    using System;
    using System.IO;
    using System.Threading.Tasks;

    internal class AuthenticatorStoreFile : IAuthenticationDetailsStore
    {
        private const string StoreFileName = "authenticationStore.txt";

        public async Task<AuthenticationDetails> GetByEmail(string email)
        {
            var fs = new FileStream(StoreFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            var sr = new StreamReader(fs);

            string line;

            while ((line = await sr.ReadLineAsync()) != null)
            {
                var details = line.Split(',');
                if (details[1].Equals(email))
                    return new AuthenticationDetails {Email = details[0], Password = details[1]};
            }

            throw new Exception();
        }

        public async void Insert(AuthenticationDetails details)
        {
            var fs = new FileStream(StoreFileName, FileMode.OpenOrCreate, FileAccess.ReadWrite);

            var sr = new StreamWriter(fs);
            await sr.WriteLineAsync(details.Email + ',' + details.Password);
        }
    }
}