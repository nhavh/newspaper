using System;
using System.Collections.Generic;
using System.Configuration;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace TMV.Static
{
    public class Helper
    {
        public static readonly string NoImageUrl = ConfigurationManager.AppSettings["NoImage"];
        public static readonly string NoImageType = ConfigurationManager.AppSettings["NoImage-Type"];
        public static readonly int NoImageSize = Convert.ToInt32(ConfigurationManager.AppSettings["NoImage-Size"]);

        private static ImageFormat NoImageFormatType
        {
            get
            {
                if (string.IsNullOrEmpty(NoImageType)) return ImageFormat.Gif;
                switch (NoImageType.ToLower())
                {
                    case "image/bmp":
                        return ImageFormat.Bmp;
                    case "image/jpeg":
                        return ImageFormat.Jpeg;
                    case "image/gif":
                        return ImageFormat.Gif;
                    case "image/png":
                        return ImageFormat.Png;
                    default:
                        return ImageFormat.Gif;
                }
            }
        }

        public static void DrawNotFoundImage(HttpContext context)
        {
            try
            {
                var photoPath = context.Server.MapPath(ConfigurationManager.AppSettings["NoImage"]);
                context.Response.ContentType = NoImageType;
                var photo = new Bitmap(photoPath);

                if (Equals(NoImageFormatType, ImageFormat.Gif))
                {
                    photo.Save(context.Response.OutputStream, NoImageFormatType);
                }
                else
                {
                    int width, height;
                    if (photo.Width > photo.Height)
                    {
                        width = NoImageSize;
                        height = photo.Height * NoImageSize / photo.Width;
                    }
                    else
                    {
                        width = photo.Width * NoImageSize / photo.Height;
                        height = NoImageSize;
                    }
                    var target = new Bitmap(width, height);
                    var graphics = Graphics.FromImage(target);

                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.SmoothingMode = SmoothingMode.HighQuality;
                    graphics.DrawImage(photo, 0, 0, width, height);
                    target.Save(context.Response.OutputStream, NoImageFormatType);

                    graphics.Dispose();
                    target.Dispose();
                }

                photo.Dispose();
            }
            catch { }
        }


    }

}