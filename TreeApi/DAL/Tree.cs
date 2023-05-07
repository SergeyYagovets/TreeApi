using System.ComponentModel.DataAnnotations;
using TreeApi.Models;

namespace TreeApi.DAL
{
    public class Tree
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Node>? Nodes { get; set; }
    }
}

