using System.Collections.Generic;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;

namespace CardGamesAPI.Services
{
    public class DeckCardsInterractor : IDeckCardsInterractor
    {
        private IDeckRepository repository;
        private ICardsHelper cardsHelper;

        public DeckCardsInterractor(IDeckRepository deckRepositoryParam, ICardsHelper cardsHelperParam)
        {
            repository = deckRepositoryParam;
            cardsHelper = cardsHelperParam;
        }

        public List<Card> Draw(CollectionDirection direction, string hash, int count = 1)
        {
            var deck = repository.GetDeck(hash);
            var drawnCards = cardsHelper.Draw(direction, deck.Cards, count);
            repository.Update(deck);

            return drawnCards;
        }

        public void Insert(string hash, CollectionDirection direction, Card card)
        {
            var deck = repository.GetDeck(hash);
            cardsHelper.Insert(direction, deck.Cards, card);
            repository.Update(deck);
        }

        public void Shuffle(string hash)
        {
            var deck = repository.GetDeck(hash);
            var cards = deck.Cards;
            cardsHelper.Shuffle(ref cards);
            repository.Update(deck);
        }
    }
}