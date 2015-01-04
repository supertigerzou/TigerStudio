using TigerStudio.Books.Services;
using TigerStudio.Books.ViewModels;
using TigerStudio.Books.ViewModels.Media;
using TigerStudio.Framework.Services;
using System.Linq;
using System.Web.Http;

namespace TigerStudio.Books.Controllers
{
    [Authorize]
    [RoutePrefix("api/Books")]
    public class BooksController : ApiController
    {
        private readonly IBookService _bookService;
        private readonly IPictureService _pictureService;

        public BooksController(IBookService bookService, IPictureService pictureService)
        {
            _bookService = bookService;
            _pictureService = pictureService;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_bookService.GetAll().Select(book => new BookViewModel
                {
                    Name = book.Name,
                    Author = book.Author.FirstName + " " + book.Author.LastName,
                    PictureModels = book.EntityEntityPictures.Select(pic => new PictureModel
                        {
                            ImageUrl = _pictureService.GetUrlByPicture(pic.EntityPicture, PictureType.Thumbnail),
                            FullSizeImageUrl = _pictureService.GetUrlByPicture(pic.EntityPicture, PictureType.Full)
                        }).ToList()
                }));
        }
    }
}
