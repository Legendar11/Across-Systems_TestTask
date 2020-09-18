using DatabaseRepository.Factories.ContextFactory;
using DatabaseRepository.Models;
using DatabaseRepository.Repositories.Base;
using DatabaseRepository.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseRepository.Repositories
{
    public class ArticleRepository : BaseRepository, IArticleRepository
    {
        public ArticleRepository(string connectionString, IRepositoryContextFactory contextFactory)
            : base(connectionString, contextFactory)
        { }

        public async Task<List<Article>> GetArticles()
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                return await context.Articles.ToListAsync();
            }
        }

        public async Task Add(Article article)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                await context.Articles.AddAsync(article);
                await context.SaveChangesAsync();
            }
        }

        public async Task Delete(Article article)
        {
            using (var context = ContextFactory.CreateDbContext(ConnectionString))
            {
                context.Articles.Remove(article);
                await context.SaveChangesAsync();
            }
        }
    }
}
