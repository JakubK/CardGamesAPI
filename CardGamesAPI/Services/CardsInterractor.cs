using System;
using System.Collections.Generic;
using System.Linq;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;

namespace CardGamesAPI.Services
{
    public class CardsInterractor
    {
        private IDeckRepository deckRepository;

        public CardsInterractor(IDeckRepository deckRepository)
        {
            this.deckRepository = deckRepository;
        }

        public List<Card> DrawFromTop(string deckHash, int count = 1)
        {
            if(count <= 0)
                return new List<Card>();

            var deck = deckRepository.GetDeck(deckHash);
            if(count > deck.Cards.Count)
                count = deck.Cards.Count;

            var cards = deck.Cards.Take(count).ToList();

            deck.Cards.RemoveRange(0,count);
            deckRepository.Update(deck);

            return cards;
        }

        public List<Card> DrawFromBottom(string deckHash, int count = 1)
        {
            if(count <= 0)
                return new List<Card>();
            
            var deck = deckRepository.GetDeck(deckHash);
            if(count > deck.Cards.Count)
                count = deck.Cards.Count;
            
            var cards = deck.Cards
                .Skip(deck.Cards.Count - count)
                .Take(count)
                .Reverse()
                .ToList();
            
            deck.Cards.RemoveRange(deck.Cards.Count - count,count);
            deckRepository.Update(deck);
            
            return cards;
        }
    }
}