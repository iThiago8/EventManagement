namespace Core.Models
{
    public class Article
    {
        public Article(string name, DateTime publicationDate, string articleAbstract, Subject subject)
        {
            Name = name;
            PublicationDate = publicationDate;
            Abstract = articleAbstract;
            Subject = subject ?? throw new ArgumentNullException(nameof(subject), "The subject can not be null");
            SubjectId = subject.Id;

        }

        public Article()
        {
            Name = string.Empty;
            Abstract = string.Empty;
            Subject = null!;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime PublicationDate { get; set; }
        public string Abstract { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<Person> Author { get; set; } = [];
        public ICollection<ArticleReview> ArticleReview { get; set; } = [];
    }
}
