using EmployeeAdminPortalAPI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeAdminPortalAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        [HttpPost]
        [Route("UploadFile")]
        public Response UploadFile([FromForm] FileModel fileModel)
        {
            Response response = new Response();
            try
            {
                string path = Path.Combine(@"D:\ylp\ASPDotNetCoreWebApiCRUD\EmployeeAdminPortalAPI\Images", fileModel.FileName);
                using(Stream stream = new FileStream(path, FileMode.Create))
                {
                    fileModel.file.CopyTo(stream);
                }
                response.StatusCode = 200;
                response.Message = "Image upload successful.";
            }
            catch(Exception ex) 
            {
                response.StatusCode = 100;
                response.Message = "Some error occured" + ex.Message;
            }

            return response;
        }
    }
}
