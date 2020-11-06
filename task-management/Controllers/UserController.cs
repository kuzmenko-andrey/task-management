using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

using System.IO;
using System.Linq;

using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private business.Domains.AccountDomain _accountDomain;
        private readonly IHostingEnvironment _environment;

        public UserController(business.Domains.AccountDomain accountDomain, IHostingEnvironment environment)
        {
            this._accountDomain = accountDomain;
            this._environment = environment;
        }

        [Route("user/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (!_accountDomain.IdExists(id))
            {
                return NotFound("Id did not exists");
            }

            var account = _accountDomain.Get(id);
            return Ok(account);
        }

        [Route("user")]
        [HttpPost]
        public IActionResult Create([FromBody] User request)
        {
            if (this._accountDomain.EmailExists(request.Email))
            {
                return Conflict("Email already exist");
            }

            this._accountDomain.Create(request);
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
            if (!_accountDomain.IdExists(request.Id))
            {
                return NotFound("Id did not found");
            }

            if (_accountDomain.EmailExists(request.Email))
            {
                return Conflict("Email already exist");
            }

            _accountDomain.Update(request);
            return Ok();
        }

        [Route("/user/{id}/upload_photo")]
        [HttpPut]
        public IActionResult UpdatePhoto(int id)
        {
            return UploadPhotoFiles(id);
        }

        [Route("user/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (!_accountDomain.IdExists(id))
            {
                return NotFound("Id did not found");
            }

            _accountDomain.Delete(id);
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

                        var account = _accountDomain.Get(id);
                        account.HasAvatar = true;
                        _accountDomain.Update(account);
                    }
                }
            }
            return Ok();
        }
    }
}
