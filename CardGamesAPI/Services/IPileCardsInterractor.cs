using System.Collections.Generic;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
    public interface IPileCardsInterractor
    {
        Pile Generate(string hash);
        List<Card> Draw(CollectionDirection direction, string hash,string pileHash, int count);
        void Insert(string hash,string pileHash, CollectionDirection direction, Card card);
        void Shuffle(string hash,string pileHash);
    }
}