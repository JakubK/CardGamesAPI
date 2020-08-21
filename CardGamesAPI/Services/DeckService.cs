using System.Collections.Generic;
using CardGamesAPI.Data;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;
using LiteDB;

namespace CardGamesAPI.Services
{
  public class DeckService
  {
    List<Deck> decks;
    LiteDatabase db;
    public DeckService(ILiteDbContext context)
    {
      decks = new List<Deck>();
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
  }
}