namespace Core.Models
{
    public class PersonSymposium
    {
        public PersonSymposium(Person person, Symposium symposium, string role)
        {
            Person = person ?? throw new ArgumentNullException(nameof(person), "The person can not be null!");
            PersonId = person.Id;
            Symposium = symposium ?? throw new ArgumentNullException(nameof(symposium), "The symposium can not be null!");
            SymposiumId = symposium.Id;
            Role = role;
        }

        public PersonSymposium()
        {
            Person = null!;
            Symposium = null!;
            Role = string.Empty;
        }

        public int PersonId { get; set; }
        public Person Person { get; set; }
        public int SymposiumId { get; set; }
        public Symposium Symposium { get; set; }
        public string Role { get; set; }
    }
}
