using apis.Data;
using apis.Dtos.Article;
using apis.Interfaces;
using apis.Mappers;
using apis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class ArticleRepository(ApplicationDbContext context) : IArticleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<ArticleDto> CreateAsync(Article articleModel)
        {
            Subject? subjectModel = await _context.Subject.FindAsync(articleModel.SubjectId);

            articleModel.Subject = subjectModel!;

            await _context.Article.AddAsync(articleModel);
            await _context.SaveChangesAsync();

            return articleModel.ToArticleDto();
        }

        public async Task<List<ArticleDto>> GetAllAsync()
        {
            return await _context.Article
                .Include(a => a.Subject)
                .Select(a => new ArticleDto
                {
                    Id = a.Id,
                    Name = a.Name,
                    Abstract = a.Abstract,
                    PublicationDate = a.PublicationDate,
                    Subject = a.Subject,
                    SubjectId = a.SubjectId
                })
                .ToListAsync();
        }

        public async Task<ArticleDto?> GetByIdAsync(int id)
        {
            var article = await _context.Article.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
                return null;
            else
                return article.ToArticleDto();
        }

        public async Task<ArticleDto?> UpdateAsync(int id, Article articleModel)
        {
            Article? existingArticle = await _context.Article.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);

            if (existingArticle == null)
                return null;

            Subject? newSubject = await _context.Subject.FindAsync(articleModel.SubjectId);

            existingArticle.Name = articleModel.Name;
            existingArticle.PublicationDate = articleModel.PublicationDate;
            existingArticle.Abstract = articleModel.Abstract;
            existingArticle.SubjectId = articleModel.SubjectId;
            existingArticle.Subject = newSubject!;

            return existingArticle.ToArticleDto();
        }

        public async Task<ArticleDto?> DeleteAsync(int id)
        {
            Article? articleModel = await _context.Article.FindAsync(id);

            if (articleModel == null)
                return null;

            _context.Remove(articleModel);
            await _context.SaveChangesAsync();

            return articleModel.ToArticleDto();
        }
    }
}
