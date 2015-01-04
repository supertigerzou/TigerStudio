using System.Data.Entity.ModelConfiguration;

namespace TigerStudio.Framework.Data.DomainModels
{
    public class EntityPictureMap : EntityTypeConfiguration<EntityPicture>
    {
        public EntityPictureMap()
        {
            // Primary Key
            HasKey(t => t.Id);

            // Properties
            Property(t => t.ThumbnailPhotoFileName)
                .HasMaxLength(50);

            Property(t => t.LargePhotoFileName)
                .HasMaxLength(50);

            // Table & Column Mappings
            ToTable("EntityPicture");
            Property(t => t.Id).HasColumnName("Id");
            Property(t => t.ThumbNailPhoto).HasColumnName("ThumbNailPhoto");
            Property(t => t.ThumbnailPhotoFileName).HasColumnName("ThumbnailPhotoFileName");
            Property(t => t.LargePhoto).HasColumnName("LargePhoto");
            Property(t => t.LargePhotoFileName).HasColumnName("LargePhotoFileName");
            Property(t => t.ModifiedDate).HasColumnName("ModifiedDate");
        }
    }
}
