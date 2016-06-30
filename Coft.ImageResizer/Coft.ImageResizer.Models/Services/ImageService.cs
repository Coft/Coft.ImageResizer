using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Text.RegularExpressions;
using System.IO;
using Coft.ImageResizer.Models.Helpers;

namespace Coft.ImageResizer.Models.Services
{
    public class ImageService
    {
        private static Regex imageRegex = new Regex(@"\.(jpe?g)|(png)$", RegexOptions.IgnoreCase);

        public static bool IsImageFilename(string filename)
        {
            return imageRegex.Match(filename).Success;
        }

        public static Bitmap ResizeImage(Image image, int width, int height)
        {
            float inputRatio = (float) image.Width / image.Height;
            float destRatio = (float) width / height;

            if (inputRatio > destRatio)
            {
                width = Math.Min(width, image.Width);
                height = (int) ((float)width / inputRatio);
            }
            else
            {
                height = Math.Min(height, image.Height);
                width = (int) ((float)height * inputRatio); 
            }
             
            var destRect = new Rectangle(0, 0, width, height);
            var destImage = new Bitmap(width, height);

            destImage.SetResolution(image.HorizontalResolution, image.VerticalResolution);

            using (var graphics = Graphics.FromImage(destImage))
            {
                graphics.CompositingMode = CompositingMode.SourceCopy;
                graphics.CompositingQuality = CompositingQuality.HighQuality;
                graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                graphics.SmoothingMode = SmoothingMode.HighQuality;
                graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;

                using (var wrapMode = new ImageAttributes())
                {
                    wrapMode.SetWrapMode(WrapMode.TileFlipXY);
                    graphics.DrawImage(image, destRect, 0, 0, image.Width, image.Height, GraphicsUnit.Pixel, wrapMode);
                }
            }

            return destImage;
        }

        public static void ResizeImage(Stream inputStream, Stream outputStream)
        {
            Image image = Image.FromStream(inputStream);
            Bitmap bitmap = ImageService.ResizeImage(image, Configuration.MaxWidth, Configuration.MaxHeight);

            using (MemoryStream newMemoryStream = new MemoryStream())
            {
                bitmap.Save(newMemoryStream, System.Drawing.Imaging.ImageFormat.Jpeg);
                newMemoryStream.Position = 0;
                newMemoryStream.CopyTo(outputStream);
            }
        }
    }
}
