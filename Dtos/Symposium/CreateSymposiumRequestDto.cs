namespace apis.Dtos.Symposium
{
    public class CreateSymposiumRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int LocationAddressId { get; set; }
        public string? Description { get; set; }
    }
}
