using System.Collections.Generic;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class DeckCardsInterractorTests
    {
        [Test]
        [TestCase(CollectionDirection.Top)]
        [TestCase(CollectionDirection.Bottom)]
        public void Draw_CallsHelperAndRepository(CollectionDirection direction)
        {
            var helperMock = new Mock<ICardsHelper>();
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck());
            var cardsFactoryMock = new Mock<ICardsFactory>();

            var interractor = new DeckCardsInterractor(repositoryMock.Object, helperMock.Object, cardsFactoryMock.Object);

            interractor.Draw(direction,It.IsAny<string>(),1);

            repositoryMock.Verify(x => x.GetDeck(It.IsAny<string>()));
            helperMock.Verify(x => x.Draw(direction, It.IsAny<List<Card>>(),1));
            repositoryMock.Verify(x => x.Update(It.IsAny<Deck>()));
        }

        [Test]
        [TestCase(CollectionDirection.Top)]
        [TestCase(CollectionDirection.Bottom)]
        public void Insert_CallsHelperAndRepository(CollectionDirection direction)
        {
            var helperMock = new Mock<ICardsHelper>();
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck());
            var cardsFactoryMock = new Mock<ICardsFactory>();

            var interractor = new DeckCardsInterractor(repositoryMock.Object, helperMock.Object, cardsFactoryMock.Object);

            interractor.Insert(It.IsAny<string>(),direction, new Card());

            repositoryMock.Verify(x => x.GetDeck(It.IsAny<string>()));
            helperMock.Verify(x => x.Insert(direction, It.IsAny<List<Card>>(), It.IsAny<Card>()));
            repositoryMock.Verify(x => x.Update(It.IsAny<Deck>()));
        }

        [Test]
        public void Shuffle_CallsHelperAndRepository()
        {
            var cards = new List<Card>();
            var helperMock = new Mock<ICardsHelper>();
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Cards = cards
                });
            var cardsFactoryMock = new Mock<ICardsFactory>();

            var interractor = new DeckCardsInterractor(repositoryMock.Object, helperMock.Object, cardsFactoryMock.Object);

            interractor.Shuffle(It.IsAny<string>());

            repositoryMock.Verify(x => x.GetDeck(It.IsAny<string>()));
            helperMock.Verify(x => x.Shuffle(ref cards));
            repositoryMock.Verify(x => x.Update(It.IsAny<Deck>()));
        }
    }
}