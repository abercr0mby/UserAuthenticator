namespace UserAuthentication.Store.Database
{
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
