using System.Threading.Tasks;

namespace UserAuthenticator
{
    public interface IAuthenticationDetailsStore
    {
        Task<AuthenticationDetails> GetByEmail(string email);

        void Insert(AuthenticationDetails details);
    }
}