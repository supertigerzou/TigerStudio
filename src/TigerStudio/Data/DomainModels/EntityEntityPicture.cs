using System;

namespace TigerStudio.Framework.Data.DomainModels
{
    public class EntityEntityPicture<TEntity, TEntityPicture> 
    {
        public EntityEntityPicture()
        {
            ModifiedDate = DateTime.Now;
        }

        public long EntityId { get; set; }
        public long EntityPictureId { get; set; }
        public bool Primary { get; set; }
        public DateTime ModifiedDate { get; set; }
        public virtual TEntity Entity { get; set; }
        public virtual TEntityPicture EntityPicture { get; set; }
    }
}
