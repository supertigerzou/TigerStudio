using System.Data.Entity.ModelConfiguration;

namespace TigerStudio.Books.Data.DomainModels
{
    public class AuthorMap : EntityTypeConfiguration<Author>
    {
        public AuthorMap()
        {
            ToTable("Author");
            HasKey(a => a.Id);
            Property(author => author.ShortDescription).HasMaxLength(500);
            Property(author => author.Description).HasMaxLength(1000);
            Property(author => author.FirstName).HasMaxLength(20);
            Property(author => author.LastName).HasMaxLength(20);
            Property(author => author.PersonType).IsFixedLength().HasMaxLength(2);

            HasOptional(a => a.LoginUser);
        }
    }
}
