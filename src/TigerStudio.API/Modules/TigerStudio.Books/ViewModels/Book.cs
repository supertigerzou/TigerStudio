
using TigerStudio.Books.ViewModels.Media;
using System.Collections.Generic;

namespace TigerStudio.Books.ViewModels
{
    public class BookViewModel
    {
        public string Name { get; set; }
        public string Author { get; set; }

        public IList<PictureModel> PictureModels { get; set; }
    }
}
