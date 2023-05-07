using AutoMapper;
using TreeApi.Models;

namespace TreeApi.Mappings
{
	public class NodesMapping : Profile
	{
        public NodesMapping()
        {
            CreateMap<NodeModel, Node>()
                .ForMember(n => n.ChildNode, opt => opt.MapFrom(nm => nm.ChildNodes)).ForMember(n => n.Tree, opt => opt.Ignore()).ForMember(n => n.ParentNode, opt => opt.Ignore());
            CreateMap<Node, NodeModel>().ForMember(nm => nm.ChildNodes, opt => opt.MapFrom(n => n.ChildNode));

            CreateMap<NodeBaseFields, Node>()
                .ForMember(n => n.Tree, opt => opt.Ignore()).ForMember(n => n.ParentNode, opt => opt.Ignore());
            CreateMap<Node, NodeBaseFields>();
        }
    }
}