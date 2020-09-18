using DatabaseRepository.Models;
using Microsoft.EntityFrameworkCore;

namespace DatabaseRepository
{
    public class RepositoryContext : DbContext
    {
        public RepositoryContext(DbContextOptions<RepositoryContext> options) : base(options)
        {

        }

        public DbSet<Article> Articles { get; set; }
    }
}
