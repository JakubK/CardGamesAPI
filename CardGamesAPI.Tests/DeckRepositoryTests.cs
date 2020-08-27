using System.Linq;
using CardGamesAPI.Exceptions;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Tests.Infrastructure;
using HashidsNet;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class DeckRepositoryTests
    {
        [Test]
        public void Insert_InsertsGivenDeck()
        {
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);
            Assert.That(deckRepository.GetDecks().Count() == 0);
            deckRepository.Insert(new Deck {});
            Assert.That(deckRepository.GetDecks().Count() == 1);
        }

        [Test]
        public void Insert_AssignsHash()
        {
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);
            var deckToInsert = new Deck();
            deckRepository.Insert(deckToInsert);

            hashidsMock.Verify(x => x.Encode(It.IsAny<int>()),Times.Once());
        }

        [Test]
        public void GetDecks_WhenNoDecksFound_ReturnsEmptySet()
        {
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);
            var decks = deckRepository.GetDecks();
            Assert.IsEmpty(decks);
        }

        [Test]
        public void GetDecks_WhenDecksFound_ReturnsAll()
        {
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);
            deckRepository.Insert(new Deck {
                
            });
            var decks = deckRepository.GetDecks();
            Assert.AreEqual(decks.Count(), 1);
        }

        [Test]
        public void GetDeck_WhenDeckFound_ReturnsIt()
        {
            var deckHash = "deckHash";
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);            
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
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);            
            Assert.Throws<DeckNotFoundException>(() => {
                deckRepository.GetDeck(deckHash);
            });
        }

        [Test]
        public void Remove_WhenDeckFound_RemovesIt()
        {
            var deckHash = "deckHash";
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);            
            deckRepository.Insert(new Deck{
                Hash = deckHash
            });
            deckRepository.Remove(deckHash);
            Assert.That(deckRepository.GetDecks().Count() == 0);
        }

        [Test]
        public void Remove_WhenDeckNotFound_Throws()
        {
            var deckHash = "deckHash";
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);            
            Assert.Throws<DeckNotFoundException>(() => deckRepository.Remove(deckHash));
        }

        [Test]
        public void Update_WhenDeckNotFound_Throws()
        {
            var deckHash = "deckHash";
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);           
            Assert.Throws<DeckNotFoundException>(() => deckRepository.Update(deckRepository.GetDeck(deckHash)));
        }

        [Test]
        public void Update_WhenDeckFound_Updates()
        {
            var deckHash = "deckHash";
            var hashidsMock = new Mock<IHashids>();
            var deckRepository = new DeckRepository(DbContextFactory.Create(), hashidsMock.Object);
            deckRepository.Insert(new Deck{
                Hash = deckHash
            });

            var deck = deckRepository.GetDeck(deckHash);
            deck.Remaining = 21;
            deckRepository.Update(deck);

            Assert.That(deckRepository.GetDeck(deckHash).Remaining == 21);
        }
    }
}