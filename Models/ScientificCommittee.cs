namespace apis.Models
{
    public class ScientificCommittee(string name, Subject subject)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public int SubjectId { get; set; } = subject?.Id ?? throw new ArgumentNullException(nameof(subject), "The subject can not be null!");
        public Subject Subject { get; set; } = subject ?? throw new ArgumentNullException(nameof(subject), "The subject can not be null!");
    }
}
