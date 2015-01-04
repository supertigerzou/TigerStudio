
using TigerStudio.Books.ViewModels.Media;
using System.Collections.Generic;

namespace TigerStudio.Books.ViewModels
{
    public class AuthorViewModel
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string ShortDescription { get; set; }
        public string Description { get; set; }
        public IList<PictureModel> PictureModels { get; set; }
    }
}
