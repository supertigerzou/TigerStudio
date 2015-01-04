using System.IO;
using System.Web.Hosting;
using TigerStudio.Framework.Data.DomainModels;
using TigerStudio.Framework.Helpers;

namespace TigerStudio.Framework.Services
{
    public class PictureService : IPictureService
    {
        private readonly IWebHelper _webHelper;

        public PictureService(IWebHelper webHelper)
        {
            _webHelper = webHelper;
        }

        public string GetUrlByPicture(EntityPicture picture, PictureType pictureType = PictureType.Thumbnail)
        {
            string fileName = string.Empty;
            var photo = new byte[0];

            switch (pictureType)
            {
                case PictureType.Thumbnail:
                    fileName = picture.ThumbnailPhotoFileName;
                    photo = picture.ThumbNailPhoto;
                    break;
                case PictureType.Full:
                    fileName = picture.LargePhotoFileName;
                    photo = picture.LargePhoto;
                    break;
            }
            var filePath = GetFileLocalPath(fileName);
            if (!File.Exists(filePath))
            {
                File.WriteAllBytes(filePath, photo);
            }

            return HostingEnvironment.ApplicationVirtualPath + "content/images/thumbs/" + fileName;
        }

        protected virtual string GetFileLocalPath(string fileName)
        {
            var filesDirectoryPath = _webHelper.MapPath("~/content/images/thumbs");

            var filePath = Path.Combine(filesDirectoryPath, fileName);
            return filePath;
        }
    }
}
