using Microsoft.AspNetCore.Mvc;

using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        [Route("task")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            return Ok();
        }

        [Route("task")]
        [HttpPost]
        public IActionResult Create(TaskItem request)
        {
            return Ok();
        }

        [Route("task")]
        [HttpPut]
        public IActionResult Update(TaskItem request)
        {
            return Ok();
        }

        [Route("/task/{id}/assign")]
        [HttpPost]
        public IActionResult Assign(string assigne)
        {
            return Ok();
        }

        [Route("/task/{id}/status")]
        [HttpPost]
        public IActionResult Status(string status)
        {
            return Ok();
        }

        [Route("task")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            return Ok();
        }

    }
}
