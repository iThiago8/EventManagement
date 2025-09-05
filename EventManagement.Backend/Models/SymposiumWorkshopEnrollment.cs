namespace Backend.Models
{
    public class SymposiumWorkshopEnrollment
    {
        public SymposiumWorkshopEnrollment(Person person, Workshop workshop, Symposium symposium, bool isLecturer, DateTime? enrollmentDate)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person), "The person can not be null!");
            PersonId = person.Id;
            Workshop = workshop ?? throw new ArgumentNullException(nameof(workshop), "The workshop can not be null!");
            WorkshopId = workshop.Id;
            Symposium = symposium ?? throw new ArgumentNullException(nameof(symposium), "The symposium can not be null!");
            SymposiumId = symposium.Id;
            IsLecturer = isLecturer;
            EnrollmentDate = enrollmentDate;
        }

        public SymposiumWorkshopEnrollment()
        {
            Person = null!;
            Workshop = null!;
            Symposium = null!;
        }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public int SymposiumId { get; set; }
        public Symposium Symposium { get; set; }
        public bool IsLecturer { get; set; }
        public DateTime? EnrollmentDate { get; set; }
    }
}
