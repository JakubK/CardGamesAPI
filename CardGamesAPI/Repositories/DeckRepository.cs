using System.Collections.Generic;
using CardGamesAPI.Data;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;
using LiteDB;

namespace CardGamesAPI.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        LiteDatabase db;
        public DeckRepository(ILiteDbContext context)
        {
            db = context.Database;
        }
        public IEnumerable<Deck> GetDecks()
        {      
            return db.GetCollection<Deck>().FindAll();
        }

        public void Insert(Deck deck)
        {
            db.GetCollection<Deck>().Insert(deck);
        }

        public Deck GetDeck(string deckHash)
        {
            return db.GetCollection<Deck>().FindOne(x => x.Hash == deckHash)
                ?? throw new DeckNotFoundException();
        }

        public void Remove(string deckHash)
        {
            db.GetCollection<Deck>().Delete(GetDeck(deckHash).Id);
        }

        public void Update(Deck deck)
        {
            db.GetCollection<Deck>().Update(deck);
        }
    }
}