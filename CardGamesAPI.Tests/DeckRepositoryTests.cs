using System.Linq;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Tests.Infrastructure;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class DeckRepositoryTests
    {
        [Test]
        public void Insert_InsertsGivenDeck()
        {
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            Assert.That(deckRepository.GetDecks().Count() == 0);
            deckRepository.Insert(new Deck {});
            Assert.That(deckRepository.GetDecks().Count() == 1);
        }

        [Test]
        public void GetDecks_WhenNoDecksFound_ReturnsEmptySet()
        {
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            var decks = deckRepository.GetDecks();
            Assert.IsEmpty(decks);
        }

        [Test]
        public void GetDecks_WhenDecksFound_ReturnsAll()
        {
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            deckRepository.Insert(new Deck {
                
            });
            var decks = deckRepository.GetDecks();
            Assert.AreEqual(decks.Count(), 1);
        }

        [Test]
        public void GetDeck_WhenDeckFound_ReturnsIt()
        {
            var deckHash = "deckHash";
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            deckRepository.Insert(new Deck {
                Hash = deckHash
            });
            var deck = deckRepository.GetDeck(deckHash);
            Assert.That(deck.Hash == deckHash);
        }

        [Test]
        public void GetDeck_WhenDeckNotFound_Throws()
        {
            var deckHash = "deckHash";
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            Assert.Throws<DeckNotFoundException>(() => {
                deckRepository.GetDeck(deckHash);
            });
        }

        [Test]
        public void RemoveDeck_WhenDeckFound_RemovesIt()
        {
            var deckHash = "deckHash";
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            deckRepository.Insert(new Deck{
                Hash = deckHash
            });
            deckRepository.Remove(deckHash);
            Assert.That(deckRepository.GetDecks().Count() == 0);
        }

        [Test]
        public void RemoveDeck_WhenDeckNotFound_Throws()
        {
            var deckHash = "deckHash";
            var deckRepository = new DeckRepository(DbContextFactory.Create());
            Assert.Throws<DeckNotFoundException>(() => deckRepository.Remove(deckHash));
        }
    }
}