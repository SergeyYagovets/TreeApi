using AutoMapper;
using TreeApi.DAL;
using TreeApi.Models;

namespace TreeApi.Mappings
{
	public class TreesMapping : Profile
	{
		public TreesMapping()
		{
			CreateMap<TreeModel, Tree>();
            CreateMap<Tree, TreeModel>();
        }
    }
}