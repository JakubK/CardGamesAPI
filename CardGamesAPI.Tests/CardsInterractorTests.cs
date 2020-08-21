using System.Collections.Generic;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class CardsInterractorTests
    {
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DrawFromTop__WhenCardsAvailable__DrawsCardsFromTop(int count)
        {            
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card(), new Card(), new Card()
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromTop(deckHash,count);

            Assert.That(cards.Count == count);
        }

        [Test]
        public void DrawFromTop__WhenCardsAvailable__DrawsCorrectly()
        {            
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card{Name="A"}, new Card{Name="B"}, new Card{Name="C"}
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromTop(deckHash,2);

            Assert.That(cards[0].Name == "A");
            Assert.That(cards[1].Name == "B");
        }

        [Test]
        public void DrawFromTop__WhenNoParam__DrawsOneCard()
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card(), new Card(), new Card()
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromTop(deckHash);

            Assert.That(cards.Count == 1);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void DrawFromTop__WhenParamNonPositive__ReturnsEmptySet(int count)
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();   
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromTop(deckHash, count);

            Assert.That(cards.Count == 0);
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        public void DrawFromTop__WhenParamGreaterThanDeckSize__ReturnsEverything(int count)
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card(), new Card(), new Card()
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromTop(deckHash,count);

            Assert.That(cards.Count == 3);
        }

        //DrawFromBottom

        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void DrawFromBottom__WhenCardsAvailable__DrawsCardsFromBottom(int count)
        {            
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card(), new Card(), new Card()
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromBottom(deckHash,count);

            Assert.That(cards.Count == count);
        }

        [Test]
        public void DrawFromBottom__WhenCardsAvailable__DrawsCorrectly()
        {            
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card{Name="A"}, new Card{Name="B"}, new Card{Name="C"}
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromBottom(deckHash,2);

            Assert.That(cards[0].Name == "C");
            Assert.That(cards[1].Name == "B");
        }

        [Test]
        public void DrawFromBottom__WhenNoParam__DrawsOneCard()
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card(), new Card(), new Card()
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromBottom(deckHash);

            Assert.That(cards.Count == 1);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        public void DrawFromBottom__WhenParamNonPositive__ReturnsEmptySet(int count)
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();   
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromBottom(deckHash, count);

            Assert.That(cards.Count == 0);
        }

        [Test]
        [TestCase(4)]
        [TestCase(5)]
        public void DrawFromBottom__WhenParamGreaterThanDeckSize__ReturnsEverything(int count)
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = new List<Card>{
                        new Card(), new Card(), new Card()
                    }
                });
            
            var deckHash = "deckHask";
            var decksService = new CardsInterractor(deckRepositoryMock.Object);
            var cards = decksService.DrawFromBottom(deckHash,count);

            Assert.That(cards.Count == 3);
        }
    }
}