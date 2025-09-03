using apis.Dtos.Subject;

namespace apis.Dtos.ScientificCommittee
{
    public class ScientificCommitteeDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int SubjectId { get; set; }
        public SubjectDto Subject { get; set; } = new();
    }
}
