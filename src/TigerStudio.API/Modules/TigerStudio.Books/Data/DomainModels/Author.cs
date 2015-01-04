using TigerStudio.Framework.Data.DomainModels;
using System.Collections.Generic;

namespace TigerStudio.Books.Data.DomainModels
{
    public class Author : Person
    {
        private ICollection<Book> _books;
        private ICollection<AuthorEntityPicture> _authorEntityPictures;

        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public virtual ICollection<AuthorEntityPicture> EntityEntityPictures
        {
            get { return _authorEntityPictures ?? (_authorEntityPictures = new List<AuthorEntityPicture>()); }
            protected set { _authorEntityPictures = value; }
        }

        public virtual ICollection<Book> Books
        {
            get { return _books ?? (_books = new List<Book>()); }
            protected set { _books = value; }
        }
    }
}
