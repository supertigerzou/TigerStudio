using System;

namespace TigerStudio.Framework.Data.DomainModels
{
    public abstract class BaseEntity
    {
        protected BaseEntity()
        {
            ModifiedDate = DateTime.Now;
            CreateDate = DateTime.Now;
        }

        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        public long Id { get; set; }
        public DateTime ModifiedDate { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
