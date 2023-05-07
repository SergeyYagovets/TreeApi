using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreeApi.DAL;
using TreeApi.Models;
using TreeApi.Services.Interfaces;

namespace TreeApi.Services.Implementation
{
	public class NodeRepository : INodeRepository
    {
        private readonly TreeDbContext _treeDbContext;
        private readonly IMapper _mapper;

        public NodeRepository(TreeDbContext treeDbContext, IMapper mapper)
        {
            _treeDbContext = treeDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Node>> GetAllAsync()
        {
            return await _treeDbContext.Nodes.Include(n => n.ChildNode).ToListAsync();
        }

        public async Task CreateNodeAsync(NodeBaseFields nodeBaseFields)
        {
            var result = _treeDbContext.Trees.FirstOrDefault(n => n.Id == nodeBaseFields.TreeId);
            if (result == null)
            {
                throw new SecureException("A tree with such a TreeId was not found");
            }

            Node node = _mapper.Map<Node>(nodeBaseFields);

            _treeDbContext.Nodes.Add(node);
            await _treeDbContext.SaveChangesAsync();
        }

        async Task INodeRepository.UpdateNodeAsync(NodeBaseFields nodeBaseFields)
        {
            if (nodeBaseFields.Id.Equals(null)
                && string.IsNullOrWhiteSpace(nodeBaseFields.Name)
                && nodeBaseFields.TreeId.Equals(null))
            {
                throw new SecureException("Required fields(Id, Name, TreeS) are empty");
            }

            var tree = _treeDbContext.Trees.FirstOrDefault(t => t.Id == nodeBaseFields.TreeId);

            if (tree == null)
            {
                throw new SecureException("Current Tree does not exist in the DataBase");
            }

            if (!nodeBaseFields.ParentNodeId.Equals(null))
            {
                var parentNode = _treeDbContext.Nodes.FirstOrDefault(n => n.Id == nodeBaseFields.ParentNodeId);
                 if (parentNode == null)
                    throw new SecureException("Current Parent Node does not exist in the DataBase");
            }

            var nodes = _treeDbContext.Nodes.FirstOrDefault(n => n.Id == nodeBaseFields.Id);
            
            if (nodes != null)
            {
                nodes.Name = nodeBaseFields.Name;
                nodes.TreeId = nodeBaseFields.TreeId;
                nodes.ParentNodeId = nodeBaseFields.ParentNodeId;
                await _treeDbContext.SaveChangesAsync();
            }
        }

        public async Task DeleteNodeAsync(int nodeId)
        {
            var node = _treeDbContext.Nodes.FirstOrDefault(n => n.Id == nodeId);
            if (node == null)
                throw new SecureException("Current Node does not exist in the DataBase");
            
            _treeDbContext.Remove(node);
            await _treeDbContext.SaveChangesAsync();
        }
    }
}