namespace apis.Dtos.Workshop
{
    public class UpdateWorkshopRequestDto
    {
        public string Name { get; set; } = string.Empty;
        public int Hours { get; set; }
        public int SubjectId { get; set; }
    }
}
