using CardGamesAPI.Data;

namespace CardGamesAPI.Tests.Infrastructure
{
    public class DbContextFactory
    {
        public static LiteDbContext Create()
        {
            return new LiteDbContext();
        }
    }
}