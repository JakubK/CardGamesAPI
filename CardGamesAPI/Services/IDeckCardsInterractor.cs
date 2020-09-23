using System.Collections.Generic;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
    public interface IDeckCardsInterractor
    {   
        List<Card> Draw(CollectionDirection direction, string hash, int count);
        Deck Create(int count);
        void Insert(string hash, CollectionDirection direction, Card card);
        void Shuffle(string hash);
    }
}