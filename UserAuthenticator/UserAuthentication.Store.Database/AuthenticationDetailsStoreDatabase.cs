namespace UserAuthentication.Store.Database
{
    using UserAuthentication.Messages;

    public sealed class AuthenticationDetailsStoreDatabase : IAuthenticationDetailsStore
    {
        public AuthenticationDetails GetByEmail(string email)
        {
            return null;
        }

        public void Insert(AuthenticationDetails details)
        {
        }
    }
}
