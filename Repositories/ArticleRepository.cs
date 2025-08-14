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

            return article.ToArticleDto();
        }

        public async Task<ArticleDto?> UpdateAsync(int id, UpdateArticleRequestDto articleDto)
        {
            Article? existingArticle = await _context.Article.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);

            if (existingArticle == null)
                return null;

            Subject? newSubject = await _context.Subject.FindAsync(articleDto.SubjectId);

            existingArticle.Name = articleDto.Name;
            existingArticle.PublicationDate = articleDto.PublicationDate;
            existingArticle.Abstract = articleDto.Abstract;
            existingArticle.SubjectId = articleDto.SubjectId;
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
