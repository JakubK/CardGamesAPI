using System.Collections.Generic;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
    public interface ICardsHelper
    {
        List<Card> Draw(CollectionDirection direction, List<Card> cards, int count);
        List<Card> Insert(CollectionDirection direction, List<Card> cards, Card cardToInsert);
        void Shuffle(ref List<Card> cards);
    }
}