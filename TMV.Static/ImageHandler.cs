using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Drawing.Imaging;
using System.Configuration;

namespace TMV.Static
{
    public class ImageHandler : IHttpHandler
    {
        private const string IMG_PARAM = "img"; // image path parameter
        private const string SIZE_PARAM = "size"; // size parameter    
        private const int DEFAULT_SIZE = 72;
        private int _sizeType = 72;
        private string _mimeText = "image/gif";
        private ImageFormat _formatType = ImageFormat.Gif;
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
        public bool IsReusable
        {
            get { return true; }
        }

        private bool IsValidImage(string fileName)
        {
            string ext = Path.GetExtension(fileName).ToLower();
            bool isValid = false;
            switch (ext)
            {
                case ".jpg":
                case ".jpeg":
                    isValid = true;
                    this._mimeText = "image/jpeg";
                    this._formatType = ImageFormat.Jpeg;
                    break;
                case ".gif":
                    isValid = true;
                    this._mimeText = "image/gif";
                    this._formatType = ImageFormat.Png;
                    break;
                case ".png":
                    isValid = true;
                    this._mimeText = "image/png";
                    this._formatType = ImageFormat.Png;
                    break;
                default:
                    isValid = false;
                    break;
            }
            return isValid;
        }

        private void SetSize(string size)
        {
            int sizeVal;
            if (!Int32.TryParse(size.Trim(), System.Globalization.NumberStyles.Integer, null, out sizeVal))
                sizeVal = DEFAULT_SIZE;

            this._sizeType = sizeVal;
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

        public void ProcessRequest(HttpContext context)
        {
            try
            {
                if (string.IsNullOrEmpty(context.Request.QueryString[SIZE_PARAM]))
                    this._sizeType = DEFAULT_SIZE;
                else
                    this.SetSize(context.Request.QueryString[SIZE_PARAM]);

                bool drawLogo = true;
                string imgPath = "";
                if (string.IsNullOrEmpty(context.Request.QueryString[IMG_PARAM]))
                {
                    imgPath = context.Request.QueryString["image"];
                    drawLogo = false;
                }
                else
                    imgPath = context.Request.QueryString[IMG_PARAM];


                if (!this.IsValidImage(imgPath))
                {
                    Helper.DrawNotFoundImage(context);
                    return;
                }
                else
                {
                    string file = imgPath.Trim().ToLower().Replace("\\", "/");
                    if (file.IndexOf("~/") != 0)
                        file = "~/" + file;

                    context.Response.ContentType = this._mimeText;
                    string cachePath = String.Empty;
                    if (drawLogo)
                        cachePath = context.Server.MapPath(file.Replace("upload", "cachelogo"));
                    else
                        cachePath = context.Server.MapPath(file.Replace("upload", "cache"));
                    string fileName = Path.GetFileName(cachePath);
                    cachePath = cachePath.Replace(fileName, this._sizeType.ToString() + fileName);
                    if (File.Exists(cachePath))
                    {
                        OutputCacheResponse(context, File.GetLastWriteTime(cachePath));
                        context.Response.WriteFile(cachePath);
                    }
                    else
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(cachePath)))
                            Directory.CreateDirectory(Path.GetDirectoryName(cachePath));
                        string photoPath = context.Server.MapPath(file);
                        if (!File.Exists(photoPath))
                        {
                            Helper.DrawNotFoundImage(context);
                            return;
                        }

                        var photo = new Bitmap(photoPath);
                        if (!drawLogo && _formatType == ImageFormat.Gif)
                        {
                            photo.Save(context.Response.OutputStream, this._formatType);
                            photo.Dispose();
                            return;
                        }
                        int width, height;
                        int maxSize = this._sizeType;
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
                        Bitmap target = new Bitmap(width, height);
                        using (Graphics graphics = Graphics.FromImage(target))
                        {
                            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                            graphics.SmoothingMode = SmoothingMode.HighQuality;
                            graphics.PixelOffsetMode = PixelOffsetMode.HighQuality;
                            graphics.CompositingQuality = CompositingQuality.HighQuality;
                            graphics.DrawImage(photo, 0, 0, width, height);

                            if (drawLogo)
                            {
                                // draw logo
                                Image logo = Bitmap.FromFile(context.Server.MapPath(ConfigurationManager.AppSettings["Logo"]));//load hinh logo
                                int w = logo.Width, h = logo.Height;
                                if (logo.Width > width)
                                {
                                    w = width - 10;
                                    h = h * width / logo.Width;
                                }

                                Bitmap TransparentLogo = new Bitmap(w, h);
                                using (Graphics TGraphics = Graphics.FromImage(TransparentLogo))
                                {
                                    TGraphics.CompositingQuality = CompositingQuality.HighQuality;
                                    TGraphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                                    TGraphics.CompositingMode = CompositingMode.SourceOver;
                                    TGraphics.DrawImage(logo, 0, 0, w, h);
                                }

                                graphics.DrawImage(TransparentLogo, width - w - 5, height - h - 3);// canh lai toa do cua logo ***
                            }

                            using (MemoryStream memoryStream = new MemoryStream())
                            {
                                //if (this._formatType == ImageFormat.Gif)
                                //{
                                //    OctreeQuantizer quantizer = new OctreeQuantizer(255, 8);
                                //    using (Bitmap quantized = quantizer.Quantize(target))
                                //    {
                                //        target.Save(memoryStream, ImageFormat.Gif);
                                //    }
                                //}
                                //else 
                                if (this._formatType == ImageFormat.Jpeg)
                                {
                                    ImageCodecInfo[] info = ImageCodecInfo.GetImageEncoders();
                                    EncoderParameters encoderParameters;
                                    encoderParameters = new EncoderParameters(1);
                                    encoderParameters.Param[0] = new EncoderParameter(Encoder.Quality, Quality);
                                    target.Save(memoryStream, info[1], encoderParameters);
                                }
                                else
                                    target.Save(memoryStream, this._formatType);

                                OutputCacheResponse(context, File.GetLastWriteTime(photoPath));
                                using (FileStream diskCacheStream = new FileStream(cachePath, FileMode.CreateNew))
                                {
                                    memoryStream.WriteTo(diskCacheStream);
                                }
                                memoryStream.WriteTo(context.Response.OutputStream);
                                memoryStream.Dispose();
                            }
                            graphics.Dispose();
                        }
                    }
                }
            }
            catch
            {
                Helper.DrawNotFoundImage(context);
            }
        }
    }
}
