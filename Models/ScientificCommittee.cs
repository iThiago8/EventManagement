namespace apis.Models
{
    public class ScientificCommittee
    {

        public ScientificCommittee(string name, Subject subject)
        {
            Name = name;
            Subject = subject ?? throw new ArgumentNullException(nameof(subject), "The subject can not be null!");
            SubjectId = subject.Id;
        }

        public ScientificCommittee()
        {
            Name = string.Empty;
            Subject = null!;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
    }
}
