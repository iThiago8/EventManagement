namespace apis.Helpers.QueryObjects
{
    public class PersonQueryObject
    {
        public string Cpf { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PhoneNumber { get; set; } = string.Empty;
        public DateTime? BirthDate { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 100;
    }
}
