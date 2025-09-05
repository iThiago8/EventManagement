namespace Backend.Models
{
    public class WorkshopSymposium
    {
        public WorkshopSymposium(Workshop workshop, Symposium symposium, DateTime startDate, DateTime endDate, int maxParticipants)
        {
            Workshop = workshop ?? throw new ArgumentNullException(nameof(workshop), "The workshop can not be null!");
            WorkshopId = workshop.Id;
            Symposium = symposium ?? throw new ArgumentNullException(nameof(symposium), "The symposium can not be null!");
            SymposiumId = symposium.Id;
            StartDate = startDate;
            EndDate = endDate;
            MaxParticipants = maxParticipants;
        }

        public WorkshopSymposium()
        {
            Workshop = null!;
            Symposium = null!;
            StartDate = default;
            EndDate = default;
        }

        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public int SymposiumId { get; set; }
        public Symposium Symposium { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int MaxParticipants { get; set; }
    }
}
