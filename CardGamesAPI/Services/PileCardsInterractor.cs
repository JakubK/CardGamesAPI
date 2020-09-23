using System.Collections.Generic;
using System.Linq;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using HashidsNet;

namespace CardGamesAPI.Services
{
    public class PileCardsInterractor : IPileCardsInterractor
    {
        IDeckRepository _deckRepository;
        ICardsHelper _cardsHelper;
        IHashids _hashids;

        public PileCardsInterractor(IDeckRepository deckRepository, ICardsHelper cardsHelper, IHashids hashids)
        {
            _deckRepository = deckRepository;
            _cardsHelper = cardsHelper;
            _hashids = hashids;
        }

        public List<Card> Draw(CollectionDirection direction, string hash,string pileHash, int count)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = deck.Piles.FirstOrDefault(x => x.Hash == pileHash);
            var drawnCards = _cardsHelper.Draw(direction, pile.Cards, count);

            _deckRepository.Update(deck);

            return drawnCards;       
        }
        public void Insert(string hash,string pileHash, CollectionDirection direction, Card card)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = deck.Piles.FirstOrDefault(x => x.Hash == pileHash);
            
            _cardsHelper.Insert(direction, pile.Cards, card);
            _deckRepository.Update(deck);
        }

        public void Shuffle(string hash,string pileHash)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = deck.Piles.FirstOrDefault(x => x.Hash == pileHash);
            var cards = pile.Cards;

            _cardsHelper.Shuffle(ref cards);
            _deckRepository.Update(deck);
        }

        public Pile Generate(string hash)
        {
            var deck = _deckRepository.GetDeck(hash);
            var pile = new Pile{
                Hash = _hashids.Encode(deck.Piles.Count)
            };
            deck.Piles.Add(pile);
            _deckRepository.Update(deck);
            return pile;
        }
    }
}