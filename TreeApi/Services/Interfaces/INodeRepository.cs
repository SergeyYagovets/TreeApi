using TreeApi.Models;

namespace TreeApi.Services.Interfaces
{
	public interface INodeRepository
	{
        Task<IEnumerable<Node>> GetAllAsync();
        Task CreateNodeAsync(NodeBaseFields nodeBaseFields);
        Task UpdateNodeAsync(NodeBaseFields nodeBaseFields);
        Task DeleteNodeAsync(int nodeId);
    }
}