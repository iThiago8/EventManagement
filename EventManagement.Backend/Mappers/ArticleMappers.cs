using apis.Dtos.Article;
using apis.Models;

namespace apis.Mappers
{
    public static class ArticleMappers
    {
        public static ArticleDto ToArticleDto(this Article articleModel)
        {
            return new ArticleDto
            {
                Id = articleModel.Id,
                Name = articleModel.Name,
                Abstract = articleModel.Abstract,
                PublicationDate = articleModel.PublicationDate,
                SubjectId = articleModel.SubjectId,
                Subject = articleModel.Subject.ToSubjectDto()
            };
        }

        public static Article ToArticleFromCreateDto(this CreateArticleRequestDto articleDto)
        {
            return new Article
            {

                Name = articleDto.Name,
                Abstract = articleDto.Abstract,
                PublicationDate = articleDto.PublicationDate,
                SubjectId = articleDto.SubjectId
            };
        }
        
        public static Article ToArticleFromUpdateDto(this UpdateArticleRequestDto articleDto)
        {
            return new Article
            {

                Name = articleDto.Name,
                Abstract = articleDto.Abstract,
                PublicationDate = articleDto.PublicationDate,
                SubjectId = articleDto.SubjectId
            };
        }

        public static Article ToArticleFromArticleDto(this ArticleDto articleDto)
        {
            return new Article
            {
                Id = articleDto.Id,
                Name = articleDto.Name,
                Abstract = articleDto.Abstract,
                PublicationDate = articleDto.PublicationDate,
                SubjectId = articleDto.SubjectId,
                Subject = articleDto.Subject.ToSubjectFromDto()
            };
        }
    }
}
