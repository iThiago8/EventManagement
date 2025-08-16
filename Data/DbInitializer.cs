namespace apis.Data
{
    public class DbInitializer
    {
        public async static void Initialize(ApplicationDbContext context)
        {
            await context.Database.EnsureCreatedAsync();
        }
    }
}
