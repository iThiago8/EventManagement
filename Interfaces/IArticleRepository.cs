using apis.Dtos.Article;
using apis.Models;

namespace apis.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(Article articleModel);
        Task<Article?> UpdateAsync(int id, Article articleModel);
        Task<Article?> DeleteAsync(int id);
        Task<bool> ArticleExists(int id);
    }
}
