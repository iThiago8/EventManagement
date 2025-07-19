using apis.Models;

namespace apis.Dtos.Article
{
    public class ArticleDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Abstract { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public Models.Subject Subject { get; set; } = new();
    }
}
