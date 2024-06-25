using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using LoggerService;
using CustomExLogNC8.Models;

namespace CustomExLogNC8.Controllers
{
    [Route("api/students")]
    [ApiController]
    public class StudentsController : ControllerBase
    {
        private readonly ILoggerManager _logger;
        public StudentsController(ILoggerManager logger) => _logger = logger;

        [HttpGet]
        public IActionResult Get()
        {
           _logger.LogInfo("Fetching all the Students from the storage");

            var students = DataManager.GetAllStudents();

            //Simulation of Exception
            throw new AccessViolationException("Violation Exception while accessing the resource.");

            _logger.LogInfo($"Returning {students.Count} students.");

            return Ok(students);
        }
    }
}
