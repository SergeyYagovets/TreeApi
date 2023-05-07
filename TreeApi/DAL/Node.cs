using System.ComponentModel.DataAnnotations;
using TreeApi.DAL;

namespace TreeApi.Models
{
	public class Node
	{
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public int TreeId { get; set; }

        public Tree Tree { get; set; }

        public int? ParentNodeId { get; set; }

        public Node? ParentNode { get; set; }

        public List<Node>? ChildNode { get; set; }
    }
}

