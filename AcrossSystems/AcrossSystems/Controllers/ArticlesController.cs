using DatabaseRepository.Models;
using DatabaseRepository.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AcrossSystems.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class ArticlesController : Controller
    {
        private IArticleRepository articleRepository;

        public ArticlesController(IArticleRepository articleRepository)
        {
            this.articleRepository = articleRepository;
        }

        [HttpGet]
        public async Task<IEnumerable<Article>> Get()
        {
            var articles = await articleRepository.GetArticles();
            return articles;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Article article)
        {
            await articleRepository.Add(article);
            return Ok(article);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var articles = await articleRepository.GetArticles();
            var article = articles.FirstOrDefault(x => x.Id == id);
            if (article == null)
            {
                return NotFound();
            }

            await articleRepository.Delete(article);
            return Ok(article);
        }
    }
}
