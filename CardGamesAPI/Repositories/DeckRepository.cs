using System.Collections.Generic;
using CardGamesAPI.Data;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;
using HashidsNet;
using LiteDB;

namespace CardGamesAPI.Repositories
{
    public class DeckRepository : IDeckRepository
    {
        LiteDatabase db;
        IHashids _hashids;
        public DeckRepository(ILiteDbContext context, IHashids hashids)
        {
            db = context.Database;
            _hashids = hashids;
        }
        public IEnumerable<Deck> GetDecks()
        {      
            return db.GetCollection<Deck>().FindAll();
        }

        public void Insert(Deck deck)
        {
            deck.Hash = _hashids.Encode(deck.Id);
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