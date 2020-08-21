using System.Collections.Generic;
using CardGamesAPI.Models;

namespace CardGamesAPI.Repositories
{
    public interface IDeckRepository
    {
        IEnumerable<Deck> GetDecks();
        void Insert(Deck deck);
        Deck GetDeck(string deckHash);
        void Remove(string deckHash);
    }
}