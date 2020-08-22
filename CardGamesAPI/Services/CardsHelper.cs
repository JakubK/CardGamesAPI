using System;
using System.Collections.Generic;
using System.Linq;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
    public class CardsHelper
    {
        public List<Card> Draw(CollectionDirection direction, List<Card> cards, int count = 1)
        {
            if(count <= 0)
                return new List<Card>();
            
            if(count > cards.Count)
                count = cards.Count;
            
            var toSkip = direction == CollectionDirection.Top ? 0 : cards.Count - count;

            var result = cards
                .Skip(toSkip)
                .Take(count)
                .ToList();
            
            cards.RemoveRange(0,count);
            return result;
        }

        public List<Card> Insert(CollectionDirection direction, List<Card> cards, Card cardToInsert)
        {
            if(direction == CollectionDirection.Top)
                cards.Insert(0,cardToInsert);
            else
                cards.Add(cardToInsert);
            return cards;
        }

        public void Shuffle(ref List<Card> cards)
        {
            cards = cards.OrderBy(i => Guid.NewGuid()).ToList();
        }
    }
}