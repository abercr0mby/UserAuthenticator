namespace UserAuthentication.Messages
{
    public interface IAuthenticationDetailsStore
    {
        AuthenticationDetails GetByEmail(string email);

        void Insert(AuthenticationDetails details);
    }
}