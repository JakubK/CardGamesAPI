using System.Linq;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;
using CardGamesAPI.Services;
using CardGamesAPI.Tests.Infrastructure;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class DeckServiceTests
    {
        [Test]
        public void Insert_InsertsGivenDeck()
        {
            var deckService = new DeckService(DbContextFactory.Create());
            Assert.That(deckService.GetDecks().Count() == 0);
            deckService.Insert(new Deck {});
            Assert.That(deckService.GetDecks().Count() == 1);
        }

        [Test]
        public void GetDecks_WhenNoDecksFound_ReturnsEmptySet()
        {
            var deckService = new DeckService(DbContextFactory.Create());
            var decks = deckService.GetDecks();
            Assert.IsEmpty(decks);
        }

        [Test]
        public void GetDecks_WhenDecksFound_ReturnsAll()
        {
            var deckService = new DeckService(DbContextFactory.Create());
            deckService.Insert(new Deck {
                
            });
            var decks = deckService.GetDecks();
            Assert.AreEqual(decks.Count(), 1);
        }

        [Test]
        public void GetDeck_WhenDeckFound_ReturnsIt()
        {
            var deckHash = "deckHash";
            var deckService = new DeckService(DbContextFactory.Create());
            deckService.Insert(new Deck {
                Hash = deckHash
            });
            var deck = deckService.GetDeck(deckHash);
            Assert.That(deck.Hash == deckHash);
        }

        [Test]
        public void GetDeck_WhenDeckNotFound_Throws()
        {
            var deckHash = "deckHash";
            var deckService = new DeckService(DbContextFactory.Create());
            Assert.Throws<DeckNotFoundException>(() => {
                deckService.GetDeck(deckHash);
            });
        }

        [Test]
        public void RemoveDeck_WhenDeckFound_RemovesIt()
        {
            var deckHash = "deckHash";
            var deckService = new DeckService(DbContextFactory.Create());
            deckService.Insert(new Deck{
                Hash = deckHash
            });
            deckService.Remove(deckHash);
            Assert.That(deckService.GetDecks().Count() == 0);
        }

        [Test]
        public void RemoveDeck_WhenDeckNotFound_Throws()
        {
            var deckHash = "deckHash";
            var deckService = new DeckService(DbContextFactory.Create());
            Assert.Throws<DeckNotFoundException>(() => deckService.Remove(deckHash));
        }
    }
}