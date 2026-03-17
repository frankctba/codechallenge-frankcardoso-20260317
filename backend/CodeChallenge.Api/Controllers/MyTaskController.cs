using CodeChallenge.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controllers
{
    [ApiController]
    [Route("api/tasks")]
    [Produces("application/json")]
    public class MyTaskController : ControllerBase
    {
        private static IList<MyTask> _tasks = new List<MyTask>();

        private readonly ILogger<MyTaskController> _logger;

        public MyTaskController(ILogger<MyTaskController> logger)
        {
            _logger = logger;
        }

        //- POST /api/tasks: Creates a new task.
        [HttpPost]
        public ActionResult<MyTask> CreateNewTask([FromBody] MyTask newTask)
        {
            if (newTask == null)
            {
                _logger.LogWarning("Received null task in POST request.");
                return BadRequest();
            }

            if (string.IsNullOrWhiteSpace(newTask.Title))
            {
                _logger.LogWarning("Received task with null or blank title in POST request.");
                return BadRequest("Title cannot be null or blank.");
            }

            newTask.Id = Guid.NewGuid();
            _tasks.Add(newTask);
            return Ok(newTask);
        }

        //- GET /api/tasks: Returns the list of all tasks.
        [HttpGet]
        public IEnumerable<MyTask> GetAllTasks()
        {
            return _tasks;
        }
    }
}
