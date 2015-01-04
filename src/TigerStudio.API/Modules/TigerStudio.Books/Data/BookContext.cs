using TigerStudio.Books.Data.DomainModels;
using TigerStudio.Framework.Data;
using TigerStudio.Framework.Data.DomainModels;
using System;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Reflection;

namespace TigerStudio.Books.Data
{
    public class BookContext : GFDbContext
    {
        public BookContext()
            : base("TigerStudio")
        {
            
        }

        public BookContext(string nameOrConnectionString) : base(nameOrConnectionString)
        {
            
        }

        public virtual IDbSet<Book> Books { get; set; }

        public virtual IDbSet<Author> Authors { get; set; }

        public virtual IDbSet<EntityPicture> EntityPictures { get; set; }
        
        public virtual IDbSet<BookEntityPicture> BookEntityPictures { get; set; }

        public virtual IDbSet<AuthorEntityPicture> AuthorEntityPictures { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //dynamically load all configuration

            var typesToRegister = Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => !String.IsNullOrEmpty(type.Namespace))
            .Where(type => type.BaseType != null && type.BaseType.IsGenericType && type.BaseType.GetGenericTypeDefinition() == typeof(EntityTypeConfiguration<>));
            foreach (var type in typesToRegister)
            {
                dynamic configurationInstance = Activator.CreateInstance(type);
                modelBuilder.Configurations.Add(configurationInstance);
            }

        }
    }
}
