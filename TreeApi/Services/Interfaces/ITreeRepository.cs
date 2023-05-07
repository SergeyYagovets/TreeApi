using TreeApi.DAL;
using TreeApi.Models;

namespace TreeApi.Services.Interfaces
{
	public interface ITreeRepository
	{
        Task<IEnumerable<Tree>> GetTreesAsync();
        Task<TreeBaseFields> CreateTreeAsync(string name);
        Task UpdateTreeAsync(TreeBaseFields treeBaseFields);
        Task DeleteTreeAsync(int treeId);
    }
}