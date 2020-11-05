using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

using System.IO;
using System.Linq;

using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private business.Domains.Account _domain;
        private readonly IHostingEnvironment _environment;

        public UserController(business.Domains.Account domain, IHostingEnvironment environment)
        {
            this._domain = domain;
            this._environment = environment;
        }

        [Route("user/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (!_domain.Exists(id))
            {
                return NotFound();
            }

            var account = _domain.Get(id);
            return Ok(account);
        }

        [Route("user")]
        [HttpPost]
        public IActionResult Create([FromBody] User request)
        {
            if (this._domain.Exists(request.Email))
            {
                return Conflict();
            }

            this._domain.Create(request);
            return Ok();
        }

        [Route("user/{id}/upload_photo")]
        [HttpPost]
        public IActionResult UploadPhoto(int id)
        {
            return UploadPhotoFiles(id);
        }

        [Route("user")]
        [HttpPut]
        public IActionResult Update([FromBody] User request)
        {
            if (!_domain.Exists(request.Id))
            {
                return NotFound();
            }

            if (_domain.Exists(request.Email))
            {
                return Conflict();
            }

            _domain.Update(request);
            return Ok();
        }

        [Route("/user/{id}/upload_photo")]
        [HttpPut]
        public IActionResult UpdatePhoto(int id)
        {
            return UploadPhotoFiles(id);
        }

        [Route("user")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (!_domain.Exists(id))
            {
                return NotFound();
            }

            _domain.Delete(id);
            return Ok();
        }

        private IActionResult UploadPhotoFiles(int id)
        {
            foreach (var image in Request.Form.Files)
            {
                if (image != null && image.Length > 0)
                {
                    //const int MaxContentLength = 1024 * 1024; //Size = 1 MB

                    var allowedFileExtensions = new[] { ".jpg" };
                    var extension = image.FileName.Substring(image.FileName.LastIndexOf('.')).ToLower();

                    if (!allowedFileExtensions.Contains(extension))
                    {
                        return NotFound("Please Upload image of type .jpg");
                    }
                    /*else if (image.Length > MaxContentLength)
                    {
                        return NotFound("Please Upload a file upto 1 mb.");
                    }*/
                    else
                    {
                        var filePath = System.IO.Path.Combine(_environment.WebRootPath, @"avatars\" + id + extension);

                        byte[] array;
                        using (MemoryStream ms = new MemoryStream())
                        {
                            image.CopyTo(ms);
                            array = ms.ToArray();
                        }
                        System.IO.File.WriteAllBytes(filePath, array);

                        var account = _domain.Get(id);
                        account.HasAvatar = true;
                        _domain.Update(account);
                    }
                }
            }
            return Ok();
        }
    }
}
