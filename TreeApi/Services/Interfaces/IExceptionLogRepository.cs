using TreeApi.DAL;

namespace TreeApi.Services.Interfaces
{
	public interface IExceptionLogRepository
	{
        Task<IEnumerable<ExceptionLog>> GetAllAsync();
        Task<ExceptionLog> GetExceptionByIdAsync(int id);
    }
}