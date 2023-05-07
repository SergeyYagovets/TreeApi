using Microsoft.EntityFrameworkCore;
using TreeApi.DAL;
using TreeApi.Services.Interfaces;

namespace TreeApi.Services.Implementation
{
	public class ExceptionLogRepository : IExceptionLogRepository
    {
        private readonly TreeDbContext _treeDbContext;

        public ExceptionLogRepository(TreeDbContext treeDbContext)
		{
            _treeDbContext = treeDbContext;
		}

        public async Task<IEnumerable<ExceptionLog>> GetAllAsync()
        {
            return await _treeDbContext.ExceptionLogs.ToListAsync();
        }

        public async Task<ExceptionLog> GetExceptionByIdAsync(int id)
        {
            var result = _treeDbContext.ExceptionLogs.FirstOrDefault(e => e.Id == id);

            if (result == null)
                throw new SecureException("Id not found");

            return await _treeDbContext.ExceptionLogs.FindAsync(id);
        }
    }
}