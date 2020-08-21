using System.IO;
using LiteDB;
using Microsoft.Extensions.Options;

namespace CardGamesAPI.Data
{
    public class LiteDbContext : ILiteDbContext
    {
        public LiteDatabase Database {get;}

        public LiteDbContext()
        {
            Database = new LiteDatabase(new MemoryStream());
        }
    }
}