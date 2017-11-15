namespace UserAuthentication.Store.Database
{
    using System.Data.Entity;

    public sealed class AuthContext : DbContext
    {
        public IDbSet<AuthenticationDetailsEntity> AuthenticationDetails { get; set; }
    }
}
