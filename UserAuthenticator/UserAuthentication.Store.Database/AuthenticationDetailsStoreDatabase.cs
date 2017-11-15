namespace UserAuthentication.Store.Database
{
    using System;
    using System.Linq;
    using UserAuthentication.Messages;

    public sealed class AuthenticationDetailsStoreDatabase : IAuthenticationDetailsStore
    {
        private AuthContext Context { get; set; }
        private Type dependency = typeof(System.Data.Entity.SqlServer.SqlProviderServices);

        public void Initialize()
        {
            this.Context = new AuthContext();
            this.Context.Database.Initialize(false);
        }

        public AuthenticationDetails GetByEmail(string email)
        {
            return this.Context.Set<AuthenticationDetailsEntity>()
                .Select(a => new AuthenticationDetails
                {
                    Email = a.Email,
                    Password = a.Password
                })
                .FirstOrDefault(a => a.Email == email);
        }

        public void Insert(AuthenticationDetails details)
        {
            this.Context.Set<AuthenticationDetailsEntity>().Add(
                new AuthenticationDetailsEntity
                {
                    Id = Guid.NewGuid(),
                    Email = details.Email,
                    Password = details.Password
                });
            this.Context.SaveChanges();
        }
    }
}
