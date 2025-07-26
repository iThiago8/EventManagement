namespace apis.Dtos.Article
{
    public class CreateArticleRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime PublicationDate { get; set; }
        public string Abstract { get; set; } = string.Empty;
        public int SubjectId { get; set; }
    }
}
