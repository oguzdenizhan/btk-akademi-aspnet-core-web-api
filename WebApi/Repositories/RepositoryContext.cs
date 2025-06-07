using Microsoft.EntityFrameworkCore;
using WebApi.Models;
using WebApi.Repostories.Config;

namespace WebApi.Repostories
{
    public class RepositoryContext:DbContext
    {
        public RepositoryContext(DbContextOptions options): base(options) 
        {
            
        }
        public DbSet<Book> Books { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new BookConfig());
        }
    }
}
