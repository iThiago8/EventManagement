namespace apis.Models
{
    public class Article(string name, DateTime publicationDate, string articleAbstract, Subject subject)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public DateTime PublicationDate { get; set; } = publicationDate;
        public string Abstract { get; set; } = articleAbstract;
        public int SubjectId { get; set; } = subject?.Id ?? throw new ArgumentNullException(nameof(subject), "The subject can not be null!");
        public Subject Subject { get; set; } = subject ?? throw new ArgumentNullException(nameof(subject), "The subject can not be null!");
    }
}
