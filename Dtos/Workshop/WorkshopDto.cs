using apis.Dtos.Subject;

namespace apis.Dtos.Workshop
{
    public class WorkshopDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }
        public int SubjectId { get; set; }
        public SubjectDto Subject { get; set; } = new();
    }
}
