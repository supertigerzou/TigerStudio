using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using TigerStudio.Framework.Data.DomainModels;

namespace TigerStudio.Framework.Data
{
    public class AuthContext : IdentityDbContext<IdentityUser>
    {
        public AuthContext()
            : base("TigerStudio")
        {

        }

        public AuthContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
            
        }

        public DbSet<Client> Clients { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }
    }
}
