namespace UserAuthentication.Store.Database
{
    using System;
    using System.Linq;
    using UserAuthentication.Messages;

    public sealed class AuthenticationDetailsStoreDatabase : IAuthenticationDetailsStore
    {
        private readonly AuthContext context = new AuthContext();

        public AuthenticationDetails GetByEmail(string email)
        {
            return this.context.Set<AuthenticationDetailsEntity>()
                .Select(a => new AuthenticationDetails
                {
                    Email = a.Email,
                    Password = a.Password
                })
                .FirstOrDefault(a => a.Email == email);
        }

        public void Insert(AuthenticationDetails details)
        {
            this.context.Set<AuthenticationDetailsEntity>().Add(
                new AuthenticationDetailsEntity
                {
                    Id = Guid.NewGuid(),
                    Email = details.Email,
                    Password = details.Password
                });
            this.context.SaveChanges();
        }
    }
}
