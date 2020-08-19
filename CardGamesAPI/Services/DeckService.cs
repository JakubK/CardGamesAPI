using System;
using System.Collections.Generic;
using System.Linq;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
  public class DeckService
  {
    List<Deck> decks;
    public DeckService()
    {
      decks = new List<Deck>();
    }
    public IEnumerable<Deck> GetDecks()
    {
      return decks ?? Enumerable.Empty<Deck>();
    }

    public void Insert(Deck deck)
    {
      decks.Add(deck);
    }

    public Deck GetDeck(string deckHash)
    {
      return decks.SingleOrDefault(x => x.Hash == deckHash) 
        ?? throw new DeckNotFoundException();
    }

    public void Remove(string deckHash)
    {
      decks.Remove(GetDeck(deckHash));
    }
  }
}