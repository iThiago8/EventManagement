namespace apis.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime BirthDate { get; set; }
        public ICollection<Article> Articles { get; set; } = [];
        public ICollection<ScientificCommittee> ScientificCommitees { get; set; } = [];
        public ICollection<PersonSymposium> PersonSymposium { get; set; } = [];
        public ICollection<SymposiumWorkshopEnrollment> SymposiumWorkshopEnrollment { get; set; } = [];
    }
}
