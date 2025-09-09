using Core.Dtos.Article;
using Core.Dtos.ArticleReview;
using Core.Models;

namespace Backend.Mappers
{
    public static class ArticleReviewMappers
    {
        public static ArticleReviewDto ToArticleReviewDto(this ArticleReview articleReviewModel)
        {
            return new ArticleReviewDto
            {
                ArticleId = articleReviewModel.ArticleId,
                Article = articleReviewModel.Article.ToArticleDto(),
                ScientificCommitteeId = articleReviewModel.ScientificCommitteeId,
                ScientificCommittee = articleReviewModel.ScientificCommittee.ToScientificCommitteeDto(),
                Grade = articleReviewModel.Grade,
                Review = articleReviewModel.Review,
                ReviewDate = articleReviewModel.ReviewDate
            };
        }

        public static ArticleReview ToArticleReviewFromCreateDto(this CreateArticleReviewRequestDto articleReviewDto)
        {
            return new ArticleReview
            {

                ArticleId = articleReviewDto.ArticleId,
                ScientificCommitteeId = articleReviewDto.ScientificCommitteeId,
                Grade = articleReviewDto.Grade,
                Review = articleReviewDto.Review,
                ReviewDate = articleReviewDto.ReviewDate
            };
        }

        public static ArticleReview ToArticleReviewFromUpdateDto(this UpdateArticleReviewRequestDto articleReviewDto)
        {
            return new ArticleReview
            {
                Grade = articleReviewDto.Grade,
                Review = articleReviewDto.Review,
                ReviewDate = articleReviewDto.ReviewDate
            };
        }

        public static ArticleReview ToArticleReviewFromArticleDto(this ArticleReviewDto articleReviewDto)
        {
            return new ArticleReview
            {
                ArticleId = articleReviewDto.ArticleId,
                ScientificCommitteeId = articleReviewDto.ScientificCommitteeId,
                Grade = articleReviewDto.Grade,
                Review = articleReviewDto.Review,
                ReviewDate = articleReviewDto.ReviewDate
            };
        }
    }
}
