using LiteDB;

namespace CardGamesAPI.Data
{
    public interface ILiteDbContext
    {
        LiteDatabase Database {get;}
    }
}