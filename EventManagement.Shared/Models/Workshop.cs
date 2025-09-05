namespace Backend.Models
{
    public class Workshop
    {
        public Workshop(string name, int hours, Subject subject)
        {
            Name = name;
            Hours = hours;
            Subject = subject ?? throw new ArgumentNullException(nameof(subject), "The subject cannot be null!");
            SubjectId = subject.Id;
        }

        public Workshop()
        {
            Name = string.Empty;
            Hours = 0;
            SubjectId = 0;
            Subject = null!;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int Hours { get; set; }
        public int SubjectId { get; set; }
        public Subject Subject { get; set; }
        public ICollection<WorkshopSymposium> WorkshopSymposium { get; set; } = [];
        public ICollection<SymposiumWorkshopEnrollment> SymposiumWorkshopEnrollment { get; set; } = [];
    }
}