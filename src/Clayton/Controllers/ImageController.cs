using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using System.Text.RegularExpressions;
using Clayton.Common;

namespace Clayton.Controllers
{
    public class ImageController : Controller
    {
        private readonly IHostingEnvironment _appEnvironment;

        public ImageController(IHostingEnvironment appEnvironment)
        {
            _appEnvironment = appEnvironment;
        }

        //[HttpPost]
        //public async Task<JsonResult> UploadImage(ICollection<IFormFile> files)
        //{
        //    List<string> imageUrls = new List<string>();
        //    foreach (var file in files)
        //    {
        //        if (file.Length > 0)
        //        {
        //            AzureHelper azureImage = new AzureHelper();
        //            var url = await azureImage.UploadImage(file);
        //            imageUrls.Add(url);
        //        }
        //    }
        //    return Json(new { success = true, urls = imageUrls });
        //}

        [HttpPost]
        public async Task<JsonResult> UploadImage()
        {
            List<string> imageUrls = new List<string>();
            if(Request.Form != null && Request.Form.Files != null)
            {
                foreach (IFormFile file in Request.Form.Files)
                {
                    if (file.Length > 0)
                    {
                        AzureHelper azureImage = new AzureHelper();
                        var url = await azureImage.UploadImage(file);
                        imageUrls.Add(url);
                    }
                }

                return Json(new { success = true, urls = imageUrls });
            }
           
            return Json(new { success = false });
        }
    }
}
