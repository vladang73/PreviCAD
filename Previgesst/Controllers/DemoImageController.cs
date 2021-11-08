using OmuModified.Drawing;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Previgesst.Controllers
{
    public class DemoImageController : Controller
    {
        // GET: DemoImage

        public static ImageCodecInfo GetEncoderInfo(string mimeType)
        {
            return ImageCodecInfo.GetImageEncoders().FirstOrDefault(t => t.MimeType == mimeType);
        }

        public ActionResult Index()
        {
            try
            {
                var repertoire = System.IO.Directory.EnumerateFiles(@"C:\bordel\imagesJosee\");

                int i = 1;
                foreach (var v in repertoire)
                {
                    using (var image = Image.FromFile(v))

                    {
                        //var img = Imager.Crop(image, new Rectangle(0, 0,1000, 1000));


                        if (image.Size.Width > image.HorizontalResolution * 1.5 || image.Size.Height > image.VerticalResolution * 1.5)
                        {
                            var resized = Imager.Resize(image, (int)((int)image.HorizontalResolution * 1.5), (int)((int)image.VerticalResolution * 1.5), true);

                            Imager.SaveJpeg(@"c:\bordel\ImagesJosee\DONE\" + i + ".jpg", resized);
                            i += 1;
                        }
                    }
                }
                
                return RedirectToAction("Index2");
            }
            catch { }

            return View();
        }
    }
}