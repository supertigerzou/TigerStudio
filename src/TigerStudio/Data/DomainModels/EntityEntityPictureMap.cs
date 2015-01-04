using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration;

namespace GreenField.Books.Data.DomainModels
{
    public class EntityEntityPictureMap : EntityTypeConfiguration<EntityEntityPicture>
    {
        public EntityEntityPictureMap()
        {
            // Primary Key
            this.HasKey(t => new { t.EntityId, t.EntityPictureId });

            // Properties
            this.Property(t => t.EntityId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            this.Property(t => t.EntityPictureId)
                .HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);

            // Table & Column Mappings
            this.ToTable("EntityEntityPicture");
            this.Property(t => t.EntityId).HasColumnName("EntityId");
            this.Property(t => t.EntityPictureId).HasColumnName("EntityPictureId");
            this.Property(t => t.Primary).HasColumnName("Primary");
            this.Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");

            // Relationships
            this.HasRequired(t => t.Entity)
                .WithMany(t => t.EntityEntityPictures)
                .HasForeignKey(d => d.EntityId);
            this.HasRequired(t => t.EntityPicture)
                .WithMany(t => t.EntityEntityPictures)
                .HasForeignKey(d => d.EntityPictureId);
        }
    }
}
