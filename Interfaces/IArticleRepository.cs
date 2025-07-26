using apis.Dtos.Article;
using apis.Models;

namespace apis.Interfaces
{
    public interface IArticleRepository
    {
        Task<List<Article>> GetAllAsync();
        Task<Article?> GetByIdAsync(int id);
        Task<Article> CreateAsync(Article articleModel);
        Task<Article?> UpdateAsync(int id, UpdateArticleRequestDto articleDto);
        Task<Article?> DeleteAsync(int id);
    }
}
