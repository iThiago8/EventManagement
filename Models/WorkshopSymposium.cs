namespace apis.Models
{
    public class WorkshopSymposium
    {
        public int WorkshopId { get; set; }
        public Workshop Workshop { get; set; }
        public int SymposiumId { get; set; }
        public Symposium Symposium { get; set; }
        
    }
}
