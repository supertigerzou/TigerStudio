using TigerStudio.Framework.Data.DomainModels;

namespace TigerStudio.Framework.Services
{
    public enum PictureType
    {
        Thumbnail,
        Full
    }
    public interface IPictureService
    {
        string GetUrlByPicture(EntityPicture picture, PictureType pictureType);
    }
}
