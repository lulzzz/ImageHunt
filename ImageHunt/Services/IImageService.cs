using ImageHunt.Model;
using ImageHunt.Model.Node;
using ImageHuntCore.Services;

namespace ImageHunt.Services
{
    public interface IImageService : IService
    {
        void AddPicture(Picture picture);
        Picture GetPictureById(int pictureId);
    }
}
