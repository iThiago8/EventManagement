namespace apis.Models
{
    public class Symposium(string name, DateTime startDate, DateTime endDate, Address locationAddress, string? description)
    {
        public int Id { get; set; }
        public string Name { get; set; } = name;
        public DateTime StartDate { get; set; } = startDate;
        public DateTime EndDate { get; set; } = endDate;
        public int LocationAddressId { get; set; } = locationAddress?.Id ?? throw new ArgumentNullException(nameof(locationAddress), "The location address can not be null!");
        public Address LocationAddress { get; set; } = locationAddress ?? throw new ArgumentNullException(nameof(locationAddress), "The location address can not be null!");
        public string? Description { get; set; } = description;
    }
}
