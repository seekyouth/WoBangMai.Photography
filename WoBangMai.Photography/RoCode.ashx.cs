using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using ThoughtWorks.QRCode.Codec;

namespace WoBangMai.Photography
{
    /// <summary>
    /// RoCode 的摘要说明
    /// </summary>
    public class RoCode : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            string action = context.Request["action"];//获取操作类型
            switch (action)
            {
                case "code": GenByQRCodeNet(context); break;//获取用户信息
            }
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }

        ///
        /// 生成二维码
        ///
        /// 二维码信息
        /// 图片
        private void GenByQRCodeNet(HttpContext context)
        {
            var codeUrl = context.Request["codeUrl"];
            var url = "http://" + context.Request.Url.Host.Trim() + codeUrl.Trim();
            string fullPath = HttpContext.Current.Server.MapPath("/favicon.ico");
            var imagemap = CreateCode_Choose(url, "BYTE","Q",8,4);
            Bitmap img = GetCodeImgUrl("aaa", imagemap, 100, 100, fullPath);
            MemoryStream ms = new MemoryStream();
            img.Save(ms, ImageFormat.Jpeg);
            context.Response.ClearContent();
            context.Response.ContentType = "jpg";
            context.Response.BinaryWrite(ms.ToArray());
        }



        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="strData">要生成的文字或者数字，支持中文。如： "4408810820 深圳－广州" 或者：4444444444</param>
        /// <param name="qrEncoding">三种尺寸：BYTE ，ALPHA_NUMERIC，NUMERIC</param>
        /// <param name="level">大小：L M Q H</param>
        /// <param name="version">版本：如 8</param>
        /// <param name="scale">比例：如 4</param>
        /// <returns></returns>
        public Bitmap CreateCode_Choose(string codeUrl, string qrEncoding, string level, int version, int scale)
        {
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();
            string encoding = qrEncoding;
            switch (encoding)
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;
                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
                default:
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;
            }

            qrCodeEncoder.QRCodeScale = scale;
            qrCodeEncoder.QRCodeVersion = version;
            switch (level)
            {
                case "L":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
                    break;
                case "M":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
                    break;
                case "Q":
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
                    break;
                default:
                    qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
                    break;
            }
            //文字生成图片
            Bitmap image = qrCodeEncoder.Encode(codeUrl);

            return image;
            
        }





        /// <summary>
        /// 生成二维码
        /// </summary>
        /// <param name="fileName">生成二维码路径</param>
        /// <param name="url">生成的内容</param>
        /// <param name="width">二维码宽</param>
        /// <param name="height">二维码高</param>
        /// <param name="userFace">需生成的Logo图片</param>
        /// <returns></returns>
        private Bitmap GetCodeImgUrl(string fileName, Bitmap bitmap, int width, int height, string userFace)
        {
            
            if (!string.IsNullOrEmpty(userFace))
            {
                try
                {
                    Bitmap bits = (Bitmap)Image.FromFile(userFace);
                    if (bits != null)
                    {
                        //剪裁一个80*80的Logo图片
                        ImageCut img = new ImageCut(0, 0, 40, 40);
                        Bitmap icon = img.KiCut(bits);
                        //userFace_b.jpg是一个边框的图片
                        Bitmap bits2 = new Bitmap((Bitmap)Image.FromFile(userFace), 40, 40);
                        if (icon != null)
                        {
                            try
                            {
                                //画了2个边框，一个是logo,一个在logo周围加了一个边框
                                using (var graphics = Graphics.FromImage(bitmap))
                                {
                                    graphics.DrawImage(bits2, (bitmap.Width - bits2.Width) / 2, (bitmap.Height - bits2.Height) / 2);
                                    graphics.DrawImage(icon, (bitmap.Width - icon.Width) / 2, (bitmap.Height - icon.Height) / 2);
                                }
                            }
                            catch (Exception ex)
                            {
                            }
                            finally
                            {
                                icon.Dispose();
                                GC.Collect();
                            }
                        }
                        bitmap.Save(fileName, ImageFormat.Jpeg);
                    }
                }
                catch (Exception ex)
                {
                }
            }
            return bitmap;
        }
    }

    public class ImageCut
    {

        /// <summary>
        /// 剪裁 -- 用GDI+
        /// </summary>
        /// <param name="b">原始Bitmap</param>
        /// <param name="StartX">开始坐标X</param>
        /// <param name="StartY">开始坐标Y</param>
        /// <param name="iWidth">宽度</param>
        /// <param name="iHeight">高度</param>
        /// <returns>剪裁后的Bitmap</returns>
        public Bitmap KiCut(Bitmap b)
        {
            if (b == null)
            {
                return null;
            }
            int w = b.Width;
            int h = b.Height;
            int intWidth = 0;
            int intHeight = 0;
            if (h * Width / w > Height)
            {
                intWidth = Width;
                intHeight = h * Width / w;
            }
            else if (h * Width / w < Height)
            {
                intWidth = w * Height / h;
                intHeight = Height;

            }
            else
            {
                intWidth = Width;
                intHeight = Height;
            }
            Bitmap bmpOut_b = new System.Drawing.Bitmap(b, intWidth, intHeight);
            w = bmpOut_b.Width;
            h = bmpOut_b.Height;

            if (X >= w || Y >= h)
            {
                return null;
            }
            if (X + Width > w)
            {
                Width = w - X;
            }
            else
            {
                X = (w - Width) / 2;
            }
            if (Y + Height > h)
            {
                Height = h - Y;
            }


            try
            {
                Bitmap bmpOut = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpOut);
                g.DrawImage(bmpOut_b, new Rectangle(0, 0, Width, Height), new Rectangle(X, Y, Width, Height), GraphicsUnit.Pixel);
                g.Dispose();
                return bmpOut;
            }
            catch
            {
                return null;
            }
        }
        public int X = 0;
        public int Y = 0;
        public int Width = 120;
        public int Height = 120;
        public ImageCut(int x, int y, int width, int heigth)
        {
            X = x;
            Y = y;
            Width = width;
            Height = heigth;
        }
    }
}