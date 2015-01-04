
using TigerStudio.Framework.Data.DomainModels;
using System.Collections.Generic;

namespace TigerStudio.Books.Data.DomainModels
{
    public class Book : BaseEntity
    {
        private ICollection<BookEntityPicture> _bookEntityPictures;

        public virtual ICollection<BookEntityPicture> EntityEntityPictures
        {
            get { return _bookEntityPictures ?? (_bookEntityPictures = new List<BookEntityPicture>()); }
            protected set { _bookEntityPictures = value; }
        }
        public string Name { get; set; }
        public long AutherId { get; set; }
        public virtual Author Author { get; set; }
    }
}
