
using Microsoft.AspNet.Identity.EntityFramework;

namespace TigerStudio.Framework.Data.DomainModels
{
    public class Person : BaseEntity
    {
        public string PersonType { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public IdentityUser LoginUser { get; set; }
    }
}
