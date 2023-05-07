using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TreeApi.DAL;
using TreeApi.Models;
using TreeApi.Services.Implementation;
using TreeApi.Services.Interfaces;

namespace TreeApi.Services
{
	public class TreeRepository : ITreeRepository
	{
        private readonly TreeDbContext _treeDbContext;
        private readonly IMapper _mapper;

        public TreeRepository(TreeDbContext treeDbContext, IMapper mapper)
        {
            _treeDbContext = treeDbContext;
            _mapper = mapper;
        }

        public async Task<IEnumerable<Tree>> GetTreesAsync()
        {
            return await _treeDbContext.Trees.Include(n => n.Nodes).ToListAsync();
        }

        async Task<TreeBaseFields> ITreeRepository.CreateTreeAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new SecureException("Fill in the name field");
            }

            var instTree = new Tree()
            {
                Name = name
            };

            _treeDbContext.Trees.Add(instTree);
            await _treeDbContext.SaveChangesAsync();

            return _mapper.Map<TreeBaseFields>(instTree); 
        }

        public async Task UpdateTreeAsync(TreeBaseFields treeBaseFields)
        {
            var result = _treeDbContext.Trees.FirstOrDefault(f => f.Id == treeBaseFields.Id);

            if (result == null)
                throw new SecureException("Current Tree does not exist in the DataBase");

            if (string.IsNullOrWhiteSpace(treeBaseFields.Name))
                throw new SecureException("Fill in the name field");

            result.Name = treeBaseFields.Name;
            await _treeDbContext.SaveChangesAsync();
        }

        public async Task DeleteTreeAsync(int treeId)
        {
            var tree = _treeDbContext.Trees.FirstOrDefault(x => x.Id == treeId);
            if (tree == null)
                throw new SecureException("Current Tree does not exist in the DataBase");
            
            _treeDbContext.Remove(tree);
            await _treeDbContext.SaveChangesAsync();
        }
    }
}