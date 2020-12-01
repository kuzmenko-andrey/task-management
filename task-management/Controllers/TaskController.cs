using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;

using task_management.business.ViewModels;

namespace task_management.Controllers
{
    [Route("api")]
    [Authorize]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private business.Domains.TaskDomain _taskDomain;
        private business.Domains.AccountDomain _accountDomain;

        public TaskController(business.Domains.TaskDomain taskDomain, business.Domains.AccountDomain accountDomain)
        {
            this._taskDomain = taskDomain;
            this._accountDomain = accountDomain;
        }

        [Route("task")]
        [HttpGet]
        public IActionResult Get(int id)
        {
            if (!_taskDomain.Exists(id))
            {
                return NotFound("Id did not found");
            }

            var task = _taskDomain.Get(id);
            return Ok(task);
        }

        [Route("task")]
        [HttpPost]
        public IActionResult Create(TaskItem request)
        {
            var accountIdString = User?.FindFirst(ClaimTypes.Name)?.Value;
            int? id = (accountIdString != null ? int.Parse(accountIdString) : (int?)null);

            if (!id.HasValue)
            {
                return NotFound("Unknown user");
            }

            string username = _accountDomain.Get(id.Value).Username;
            if (string.IsNullOrEmpty(username))
            {
                return NotFound("Username is empty");
            }
            request.Assignee = username;

            this._taskDomain.Create(request);
            return Ok();
        }

        [Route("task/{id}")]
        [HttpPut]
        public IActionResult Update(int id, TaskItem request)
        {
            if (!_taskDomain.Exists(id))
            {
                return NotFound("Id did not found");
            }

            _taskDomain.Update(request);
            return Ok();
        }

        [Route("/task/{id}/assign")]
        [HttpPost]
        public IActionResult Assign(int id, string assignee)
        {
            if (!_taskDomain.Exists(id))
            {
                return NotFound("Id did not found");
            }

            if (_accountDomain.Get().Any(a => a.Username == assignee))
            {
                return NotFound("Assigne did not found");
            }

            var task = _taskDomain.Get(id);
            task.Assignee = assignee;
            _taskDomain.Update(task);

            return Ok();
        }

        [Route("/task/{id}/status")]
        [HttpPost]
        public IActionResult Status(int id, string status)
        {
            if (!_taskDomain.Exists(id))
            {
                return NotFound();
            }

            var task = _taskDomain.Get(id);
            task.Status = status;
            _taskDomain.Update(task);

            return Ok();
        }

        [Route("task/{id}")]
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            if (!_taskDomain.Exists(id))
            {
                return NotFound("Id did not found");
            }

            _taskDomain.Delete(id);
            return Ok();
        }

    }
}
