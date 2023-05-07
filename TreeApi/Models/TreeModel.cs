namespace TreeApi.Models
{
	public class TreeModel : TreeBaseFields
    {
        public List<NodeBaseFields>? Nodes { get; set; }
    }

    public class TreeBaseFields
    {
        public int Id { get; set; }
     
        public string Name { get; set; }
    }
}