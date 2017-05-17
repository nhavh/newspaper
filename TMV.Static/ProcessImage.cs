using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Drawing;
using System.Configuration;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;

namespace TMV.Static
{
    public class ProcessImage : IHttpHandler
    {
        private int _quality = -1;
        private int Quality
        {
            get
            {
                if (_quality == -1)
                    return (_quality = int.Parse(ConfigurationManager.AppSettings["Quality"]));
                return _quality;
            }
        }

        public void ProcessRequest(HttpContext context)
        {

            string url = context.Request.CurrentExecutionFilePath.ToLower();
            string imgPath = "";
            bool drawLogo = false;
            if (url.Contains("/img/"))
            {
                imgPath = url.Replace("/img/", "~/upload/");
                drawLogo = true;
            }
            else if (url.Contains("/image/"))
            {
                imgPath = url.Replace("/image/", "~/upload/");
            }
            if (imgPath != "")
            {
                string cachePath;
                cachePath = context.Server.MapPath(drawLogo ? imgPath.Replace("upload", "cachelogo") :
                    imgPath.Replace("upload", "cache"));
                var formatType = ImageFormat.Jpeg;
                switch (Path.GetExtension(imgPath))
                {
                    case ".bmp":
                        context.Response.ContentType = "image/bmp";
                        formatType = ImageFormat.Bmp;
                        break;
                    case ".jpg":
                    case ".jpeg":
                        context.Response.ContentType = "image/jpeg";
                        formatType = ImageFormat.Jpeg;
                        break;
                    case ".gif":
                        context.Response.ContentType = "image/gif";
                        formatType = ImageFormat.Gif;
                        break;
                    case ".png":
                        context.Response.ContentType = "image/png";
                        formatType = ImageFormat.Png;
                        break;
                }

                if (File.Exists(cachePath))
                {
                    OutputCacheResponse(context, File.GetLastWriteTime(cachePath));
                    context.Response.WriteFile(cachePath);
                }
                else
                {
                    var name = Path.GetFileName(imgPath);
                    if (string.IsNullOrEmpty(name))
                    {
                        //throw new HttpException(404, "Image not found.");
                        Helper.DrawNotFoundImage(context);
                        return;
                    }

                    var filename = name.Substring(name.IndexOf("_", StringComparison.Ordinal) + 1);
                    var photoPath = context.Server.MapPath(imgPath.Substring(0, imgPath.LastIndexOf("/", StringComparison.Ordinal) + 1) + filename);
                    if (!File.Exists(photoPath))
                    {
                        //throw new HttpException(404, "Image not found.");
                        Helper.DrawNotFoundImage(context);
                        return;
                    }

                    var photo = new Bitmap(photoPath);
                    if (!drawLogo && Equals(formatType, ImageFormat.Gif))
                    {
                        photo.Save(context.Response.OutputStream, formatType);
                        photo.Dispose();
                        return;
                    }

                    if (!string.IsNullOrEmpty(cachePath) && !Directory.Exists(Path.GetDirectoryName(cachePath)))
                        Directory.CreateDirectory(Path.GetDirectoryName(cachePath));


                    var maxSize = 200;
                    if (name.IndexOf("_") > 0)
                        maxSize = Convert.ToInt32(name.Substring(0, name.IndexOf("_")));

                    int width, height;
                    if (photo.Width > photo.Height)
                    {
                        if (photo.Width > maxSize)
                        {
                            width = maxSize;
                            height = photo.Height * maxSize / photo.Width;
                        }
                        else
                        {
                            width = photo.Width;
                            height = photo.Height;
                        }
                    }
                    else
                    {
                        if (photo.Height > maxSize)
                        {
                            width = photo.Width * maxSize / photo.Height;
                            height = maxSize;
                        }
                        else
                        {
                            width = photo.Width;
                            height = photo.Height;
                        }
                    }
                    var target = new Bitmap(width, height);
                    using (var graphics = Graphics.FromImage(target))
                    {
                        graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                        graphics.SmoothingMode = SmoothingMode.HighQuality;
                        graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                        graphics.CompositingQuality = CompositingQuality.HighQuality;
                        graphics.DrawImage(photo, 0, 0, width, height);

                        if (drawLogo)
                        {
                            // draw logo
                            var logo = Bitmap.FromFile(context.Server.MapPath(ConfigurationManager.AppSettings["Logo"]));//load hinh logo
                            int w = logo.Width, h = logo.Height;
                            if (logo.Width > width)
                            {
                                w = width - 10;
                                h = h * width / logo.Width;
                            }

                            var transparentLogo = new Bitmap(w, h);
                            using (var transGraphics = Graphics.FromImage(transparentLogo))
                            {
                                transGraphics.CompositingQuality = CompositingQuality.HighSpeed;
                                transGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                transGraphics.CompositingMode = CompositingMode.SourceOver;
                                transGraphics.DrawImage(logo, 0, 0, w, h);
                            }

                            graphics.DrawImage(transparentLogo, width - w - 5, height - h - 3);// canh lai toa do cua logo ***
                        }

                        using (var memoryStream = new MemoryStream())
                        {
                            if (Equals(formatType, ImageFormat.Jpeg))
                            {
                                var info = ImageCodecInfo.GetImageEncoders();
                                var encoderParameters = new EncoderParameters(1);
                                encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Quality);
                                target.Save(memoryStream, info[1], encoderParameters);
                            }
                            else
                                target.Save(memoryStream, formatType);

                            OutputCacheResponse(context, File.GetLastWriteTime(photoPath));
                            using (var diskCacheStream = new FileStream(cachePath, FileMode.CreateNew))
                            {
                                memoryStream.WriteTo(diskCacheStream);
                                diskCacheStream.Dispose();
                            }
                            memoryStream.WriteTo(context.Response.OutputStream);
                            memoryStream.Dispose();
                        }

                        graphics.Dispose();
                        target.Dispose();
                        photo.Dispose();
                    }
                }
            }
        }

        private static void OutputCacheResponse(HttpContext context, DateTime lastModified)
        {
            HttpCachePolicy cachePolicy = context.Response.Cache;
            cachePolicy.SetCacheability(HttpCacheability.Public);
            cachePolicy.VaryByParams["*"] = true;
            cachePolicy.SetOmitVaryStar(true);
            cachePolicy.SetExpires(DateTime.Now + TimeSpan.FromDays(365));
            cachePolicy.SetValidUntilExpires(true);
            cachePolicy.SetLastModified(lastModified);
        }

        public bool IsReusable
        {
            get { return true; }
        }
    }
}
