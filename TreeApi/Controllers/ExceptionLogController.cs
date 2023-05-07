using Microsoft.AspNetCore.Mvc;
using TreeApi.DAL;
using TreeApi.Services.Interfaces;

namespace TreeApi.Controllers
{
    [Route("api/[controller]")]
    public class ExceptionLogController : Controller
	{
        private readonly IExceptionLogRepository _exceptionLogRepository;

        public ExceptionLogController(IExceptionLogRepository exceptionLogRepository)
		{
            _exceptionLogRepository = exceptionLogRepository;
		}

        [HttpGet("GetException")]
        public async Task<ActionResult<IEnumerable<ExceptionLog>>> GetAllAsync()
        {
            var exception = await _exceptionLogRepository.GetAllAsync();
            return Ok(exception);
        }

        [HttpGet("GetException{id}")]
        public async Task<ActionResult<Tree>> GetExceptionByIdAsync(int id)
        {
            var tree = await _exceptionLogRepository.GetExceptionByIdAsync(id);
            return Ok(tree);
        }
    }
}