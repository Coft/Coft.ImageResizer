using System.Drawing;
using System.IO;

namespace Coft.ImageResizer.Models.Services
{
    public interface IImageService
    {
        bool IsImageFilename(string filename);
        void ResizeImage(Stream inputStream, Stream outputStream);
        Bitmap ResizeImage(Image image, int width, int height);
    }
}