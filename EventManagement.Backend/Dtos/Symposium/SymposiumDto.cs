using apis.Dtos.Address;

namespace apis.Dtos.Symposium
{
    public class SymposiumDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationAddressId { get; set; }
        public AddressDto LocationAddress { get; set; } = new();
        public string? Description { get; set; }
    }
}
