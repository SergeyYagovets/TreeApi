using Microsoft.EntityFrameworkCore;
using TreeApi.Models;

namespace TreeApi.DAL
{
	public class TreeDbContext : DbContext
	{
        public TreeDbContext(DbContextOptions<TreeDbContext> options) : base(options)
        {
        }

        public DbSet<Tree> Trees { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<ExceptionLog> ExceptionLogs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Node>()
               .HasOne(n => n.Tree)
               .WithMany(t => t.Nodes)
               .HasForeignKey(n => n.TreeId)
               .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Node>()
                .HasOne(n => n.ParentNode)
                .WithMany(n => n.ChildNode)
                .HasForeignKey(n => n.ParentNodeId)
                .OnDelete(DeleteBehavior.Cascade);

           
        }
    }
}

