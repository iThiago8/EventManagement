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
        public async Task<bool> ArticleExists(int id)
        {
            return await context.Article.AnyAsync(a => a.Id == id);
        }

        public async Task<Article> CreateAsync(Article articleModel)
        {
            var subjectModel = await context.Subject.FindAsync(articleModel.SubjectId);

            articleModel.Subject = subjectModel!;

            await context.Article.AddAsync(articleModel);
            await context.SaveChangesAsync();

            return articleModel;
        }

        public async Task<List<Article>> GetAllAsync()
        {
            return await context.Article
                .Include(a => a.Subject)
                .Select(a => new Article
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

        public async Task<Article?> GetByIdAsync(int id)
        {
            var article = await context.Article.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);

            if (article == null)
                return null;
            else
                return article;
        }

        public async Task<Article?> UpdateAsync(int id, Article articleModel)
        {
            var existingArticle = await context.Article.Include(a => a.Subject).FirstOrDefaultAsync(a => a.Id == id);

            if (existingArticle == null)
                return null;

            Subject? newSubject = await context.Subject.FindAsync(articleModel.SubjectId);

            existingArticle.Name = articleModel.Name;
            existingArticle.PublicationDate = articleModel.PublicationDate;
            existingArticle.Abstract = articleModel.Abstract;
            existingArticle.SubjectId = articleModel.SubjectId;
            existingArticle.Subject = newSubject!;

            return existingArticle;
        }

        public async Task<Article?> DeleteAsync(int id)
        {
            var articleModel = await context.Article.FindAsync(id);

            if (articleModel == null)
                return null;

            context.Remove(articleModel);
            await context.SaveChangesAsync();

            return articleModel;
        }
    }
}
