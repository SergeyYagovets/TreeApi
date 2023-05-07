namespace TreeApi.Models
{
	public class NodeModel : NodeBaseFields
    {
        public List<NodeModel>? ChildNodes;
    }

    public class NodeBaseFields
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int TreeId { get; set; }

        public int? ParentNodeId { get; set; }
    }
}