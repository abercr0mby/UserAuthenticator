using System.Threading.Tasks;

namespace UserAuthentication
{
    public interface IAuthenticationDetailsStore
    {
        AuthenticationDetails GetByEmail(string email);

        void Insert(AuthenticationDetails details);
    }
}