using TigerStudio.Books.Services;
using TigerStudio.Books.ViewModels;
using TigerStudio.Books.ViewModels.Media;
using TigerStudio.Framework.Services;
using System.Linq;
using System.Web.Http;

namespace TigerStudio.Books.Controllers
{
    [RoutePrefix("api/Authors")]
    public class AuthorsController : ApiController
    {
        private readonly IAuthorService _authorService;
        private readonly IPictureService _pictureService;

        public AuthorsController(IAuthorService authorService, IPictureService pictureService)
        {
            _authorService = authorService;
            _pictureService = pictureService;
        }

        [Route("")]
        public IHttpActionResult Get()
        {
            return Ok(_authorService.GetAll().Select(author => new AuthorViewModel
                {
                    Id = author.Id,
                    Name = string.Format("{0} {1}", author.FirstName, author.LastName),
                    ShortDescription = author.ShortDescription,
                    Description = author.Description,
                    PictureModels = author.EntityEntityPictures.Select(pic => new PictureModel
                        {
                            ImageUrl = _pictureService.GetUrlByPicture(pic.EntityPicture, PictureType.Thumbnail),
                            FullSizeImageUrl = _pictureService.GetUrlByPicture(pic.EntityPicture, PictureType.Full)
                        }).ToList()
                }));
        }

        // GET api/authors/5
        [Route("{id:long}")]
        public IHttpActionResult Get(long id)
        {
            var author = _authorService.GetById(id);

            return Ok(new AuthorViewModel
            {
                Id = author.Id,
                Name = string.Format("{0} {1}", author.FirstName, author.LastName),
                Description = author.Description,
                PictureModels = author.EntityEntityPictures.Select(pic => new PictureModel
                {
                    ImageUrl = _pictureService.GetUrlByPicture(pic.EntityPicture, PictureType.Thumbnail),
                    FullSizeImageUrl = _pictureService.GetUrlByPicture(pic.EntityPicture, PictureType.Full)
                }).ToList()
            });
        }
    }
}
