using DatabaseRepository.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DatabaseRepository.Repositories.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetArticles();

        Task Add(Article article);

        Task Delete(Article article);
    }
}
