using System.Data.Entity.ModelConfiguration;

namespace TigerStudio.Books.Data.DomainModels
{
    public class BookMap : EntityTypeConfiguration<Book>
    {
        public BookMap()
        {
            ToTable("Book");
            HasKey(b => b.Id);

            HasRequired(b => b.Author)
                .WithMany(a => a.Books)
                .HasForeignKey(b => b.AutherId);

        }
    }
}
