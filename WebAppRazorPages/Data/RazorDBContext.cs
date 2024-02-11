using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;
using WebAppRazorPages.Models;

namespace WebAppRazorPages.Data
{
    public class RazorDBContext:DbContext
    {
        public RazorDBContext(DbContextOptions<RazorDBContext> options) : base(options)
        {
                
        }        
        public DbSet<Category> Categories { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Category>().HasData(
                new Category { ID = 1, Name = "Action", DisplayOrder = 1 },
                new Category { ID = 2, Name = "Science", DisplayOrder = 2 },
                new Category { ID = 3, Name = "History", DisplayOrder = 3 }
                );
        }
    }
}
