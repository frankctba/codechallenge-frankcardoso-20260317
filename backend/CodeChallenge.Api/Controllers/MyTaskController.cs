using CodeChallenge.Api.Models;
using Microsoft.AspNetCore.Mvc;

namespace CodeChallenge.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MyTaskController : ControllerBase
    {
        private static IList<MyTask> _tasks = new List<MyTask>();

        private readonly ILogger<MyTaskController> _logger;

        public MyTaskController(ILogger<MyTaskController> logger)
        {
            _logger = logger;
        }

        //- POST /api/tasks: Creates a new task.
        [HttpPost(Name = "/api/tasks")]
        public ActionResult<MyTask> Post([FromBody] MyTask newTask)
        {
            if (newTask == null)
            {
                _logger.LogWarning("Received null task in POST request.");
                return BadRequest();
            }

            //Validate that title is not null or blank

            newTask.Id = Guid.NewGuid();
            _tasks.Add(newTask);
            return Ok(newTask);
        }

        //- GET /api/tasks: Returns the list of all tasks.
        [HttpGet(Name = "/api/tasks")]
        public IEnumerable<MyTask> GetAll()
        {
            return _tasks;
        }
    }
}
