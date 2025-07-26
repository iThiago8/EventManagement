using apis.Data;
using apis.Dtos.Article;
using apis.Interfaces;
using apis.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace apis.Repositories
{
    public class ArticleRepository(ApplicationDbContext context) : IArticleRepository
    {
        private readonly ApplicationDbContext _context = context;

        public async Task<Article> CreateAsync(Article articleModel)
        {
            Subject? subjectModel = await _context.Subject.FindAsync(articleModel.SubjectId);

            articleModel.Subject = subjectModel!;

            await _context.Article.AddAsync(articleModel);
            await _context.SaveChangesAsync();

            return articleModel;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await _context.Article.Include(a => a.Subject).ToListAsync();
        }

        public async Task<Article?> GetByIdAsync(int id)
        {
            return await _context.Article.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<Article?> UpdateAsync(int id, UpdateArticleRequestDto articleDto)
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

            return existingArticle;
        }

        public async Task<Article?> DeleteAsync(int id)
        {
            Article? articleModel = await _context.Article.FindAsync(id);

            if (articleModel == null)
                return null;

            _context.Remove(articleModel);
            await _context.SaveChangesAsync();

            return articleModel;
        }
    }
}
