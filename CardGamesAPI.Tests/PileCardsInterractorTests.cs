using System.Collections.Generic;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class PileCardsInterractorTests
    {
        [Test]
        [TestCase(CollectionDirection.Top)]
        [TestCase(CollectionDirection.Bottom)]
        public void Draw_CallsHelperAndRepository(CollectionDirection direction)
        {
            var helperMock = new Mock<ICardsHelper>();
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(It.IsAny<string>()))
                .Returns(new Deck{
                    Piles = new List<Pile>{
                        new Pile{
                            Name = "First",
                            Cards = new List<Card>()
                        }
                    }
                });

            var interractor = new PileCardsInterractor(repositoryMock.Object, helperMock.Object);

            interractor.Draw(direction,It.IsAny<string>(), "First",1);

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
                .Returns(new Deck{
                    Piles = new List<Pile>{
                        new Pile{
                            Name = "First",
                            Cards = new List<Card>()
                        }
                    }
                });

            var interractor = new PileCardsInterractor(repositoryMock.Object, helperMock.Object);

            interractor.Insert(It.IsAny<string>(),"First",direction, new Card());

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
                    Piles = new List<Pile>{
                        new Pile{
                            Name = "First",
                            Cards = cards
                        }
                    }
                });

            var interractor = new PileCardsInterractor(repositoryMock.Object, helperMock.Object);

            interractor.Shuffle(It.IsAny<string>(),"First");

            repositoryMock.Verify(x => x.GetDeck(It.IsAny<string>()));
            helperMock.Verify(x => x.Shuffle(ref cards));
            repositoryMock.Verify(x => x.Update(It.IsAny<Deck>()));
        }
    }
}