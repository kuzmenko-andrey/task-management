using Microsoft.AspNetCore.Mvc;
using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        [Route("user/{id}")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [Route("user")]
        [HttpPost]
        public IActionResult Create([FromBody] User request)
        {
            return Ok();
        }

        [Route("/user/{id}/upload_photo")]
        [HttpPost]
        public IActionResult UploadPhoto(int id)
        {
            return Ok();
        }

        [Route("user")]
        [HttpPut]
        public IActionResult Update([FromBody] User request)
        {
            return Ok();
        }

        [Route("/user/{id}/upload_photo")]
        [HttpPut]
        public IActionResult UpdatePhoto(int id)
        {
            return Ok();
        }

        [Route("user")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }
    }
}
