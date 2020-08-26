using System;
using System.Collections.Generic;
using System.Linq;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;

namespace CardGamesAPI.Services
{
    public class PileCardsInterractor
    {
        private IDeckRepository _deckRepository;
        private ICardsHelper _cardsHelper;

        public PileCardsInterractor(IDeckRepository deckRepository, ICardsHelper cardsHelper)
        {
            _deckRepository = deckRepository;
            _cardsHelper = cardsHelper;
        }

        public List<Card> Draw(CollectionDirection direction, string hash,string pileHash, int count)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = deck.Piles.FirstOrDefault(x => x.Name == pileHash);
            var drawnCards = _cardsHelper.Draw(direction, pile.Cards, count);

            _deckRepository.Update(deck);

            return drawnCards;       
        }
        public void Insert(string hash,string pileHash, CollectionDirection direction, Card card)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = deck.Piles.FirstOrDefault(x => x.Name == pileHash);
            
            _cardsHelper.Insert(direction, pile.Cards, card);
            _deckRepository.Update(deck);
        }

        public void Shuffle(string hash,string pileHash)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = deck.Piles.FirstOrDefault(x => x.Name == pileHash);
            var cards = pile.Cards;

            _cardsHelper.Shuffle(ref cards);
            _deckRepository.Update(deck);
        }
    }
}