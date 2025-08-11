using CodePulse.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodePulse.EntityFrameworkCore.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }   
        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<BlogImage> BlogImages { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<ChatMessage> ChatMessages { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Self-referencing relationship for Comments
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.ParentComment)
                .WithMany(c => c.Replies)
                .HasForeignKey(c => c.ParentId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship between Comments and BlogPost
            modelBuilder.Entity<Comments>()
                .HasOne(c => c.Post)
                .WithMany(p => p.Comments) // You'll need ICollection<Comments> in BlogPost
                .HasForeignKey(c => c.PostId)
                .OnDelete(DeleteBehavior.Cascade);
        }

    }
}
