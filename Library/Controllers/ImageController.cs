using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Library.AzureStorage;

namespace BlobStorageDemo.Controllers
{
    public class ImageController : Controller
    {
        ImageService imageService = new ImageService();

        // GET: Image  
        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Upload(HttpPostedFileBase photo, string container)
        {
            var imageUrl = await imageService.UploadImageAsync(photo, container);
            TempData["LatestImage"] = imageUrl.ToString();
            return new EmptyResult();
        }

        public ActionResult LatestImage()
        {
            var latestImage = string.Empty;
            if (TempData["LatestImage"] != null)
            {
                ViewBag.LatestImage = Convert.ToString(TempData["LatestImage"]);
            }

            return View();
        }
    }
}