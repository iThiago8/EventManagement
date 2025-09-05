namespace Backend.Models
{
    public class Symposium
    {
        public Symposium(string name, DateTime startDate, DateTime endDate, Address locationAddress, string? description)
        {
            Name = name;
            StartDate = startDate;
            EndDate = endDate;
            Description = description;
            LocationAddress = locationAddress ?? throw new ArgumentNullException(nameof(locationAddress), "The location address cannot be null!");
            LocationAddressId = locationAddress.Id;
        }

        public Symposium()
        {
            Name = string.Empty;
            StartDate = default;
            EndDate = default;
            LocationAddressId = 0;
            LocationAddress = null!;

        }

        public int Id { get; set; }
        public string Name { get; set; } 
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationAddressId { get; set; }
        public Address LocationAddress { get; set; }
        public string? Description { get; set; }
        public ICollection<PersonSymposium> PersonSymposium { get; set; } = [];
        public ICollection<WorkshopSymposium> WorkshopSymposium { get; set; } = [];
        public ICollection<SymposiumWorkshopEnrollment> SymposiumWorkshopEnrollment { get; set; } = [];
    }
}