using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace InternalServices.Controllers
{
    public class ImageController : ApiController
    {
        [HttpPost]
        [Route("api/Image/Perfil")]
        public async Task<string> Perfil()
        {
            var root = HttpContext.Current.Server.MapPath("~/App_Data");
            var provider = new MultipartFormDataStreamProvider(root);

            try
            {
                await Request.Content.ReadAsMultipartAsync(provider);

                foreach(var img in provider.FileData)
                {
                    var name = img.Headers.ContentDisposition.FileName;

                    name = name.Trim('"');

                    var localImgName = img.LocalFileName;
                    var imgPath = Path.Combine(root, name);

                    File.Move(localImgName, imgPath);    
                }

                return "true";
            }
            catch(Exception e)
            {
                return e.ToString();
            }
        }
    }
}
