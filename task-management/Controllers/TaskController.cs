using Microsoft.AspNetCore.Mvc;

using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private business.Domains.TaskDomain _domain;

        public TaskController(business.Domains.TaskDomain domain)
        {
            this._domain = domain;
        }

        [Route("task")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (!_domain.Exists(id))
            {
                return NotFound();
            }

            var task = _domain.Get(id);
            return Ok(task);
        }

        [Route("task")]
        [HttpPost]
        public IActionResult Create(TaskItem request)
        {
            this._domain.Create(request);
            return Ok();
        }

        [Route("task")]
        [HttpPut]
        public IActionResult Update(TaskItem request)
        {
            if (!_domain.Exists(request.Id))
            {
                return NotFound();
            }

            _domain.Update(request);
            return Ok();
        }

        [Route("/task/{id}/assign")]
        [HttpPost]
        public IActionResult Assign(int id, string assigne)
        {
            if (!_domain.Exists(id))
            {
                return NotFound();
            }

            var task = _domain.Get(id);
            task.Assigne = assigne;
            _domain.Update(task);

            return Ok();
        }

        [Route("/task/{id}/status")]
        [HttpPost]
        public IActionResult Status(int id, string status)
        {
            if (!_domain.Exists(id))
            {
                return NotFound();
            }

            var task = _domain.Get(id);
            task.Status = status;
            _domain.Update(task);

            return Ok();
        }

        [Route("task")]
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

    }
}
