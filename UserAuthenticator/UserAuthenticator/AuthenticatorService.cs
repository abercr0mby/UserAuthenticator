namespace UserAuthenticator
{
    using System;
    using System.Threading.Tasks;

    public class AuthenticationService
    {
        private IAuthenticationDetailsStore Store { get; } = new AuthenticatorStoreFile();

        public async void Register(string email, string password)
        {
            //ENCRYPT PASSWORD

            if (await this.Store.GetByEmail(email) != null)
            {
                throw new Exception();
            }
            
            this.Store.Insert(new AuthenticationDetails {Email = email, Password = password});
        }

        public async Task<bool> Login(string email, string password)
        {
            var user = await this.Store.GetByEmail(email);

            if (user == null)
            {
                throw new Exception();
            }

            //ENCRYPT EMAIL
            if (email == user.Email)
            {
                return true;
            }

            return false;
        }
    }
}
