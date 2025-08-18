using apis.Dtos.Article;
using apis.Models;

namespace apis.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<ArticleDto>> GetAllAsync();
        Task<ArticleDto?> GetByIdAsync(int id);
        Task<ArticleDto> CreateAsync(Article articleModel);
        Task<ArticleDto?> UpdateAsync(int id, Article articleModel);
        Task<ArticleDto?> DeleteAsync(int id);
    }
}
