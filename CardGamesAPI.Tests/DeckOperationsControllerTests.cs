using System.Collections.Generic;
using AutoMapper;
using CardGamesAPI.Contracts.Requests;
using CardGamesAPI.Contracts.Responses;
using CardGamesAPI.Controllers;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class DeckOperationsControllerTests
    {
        [Test]
        public void CreateDeck_ReturnsDeck()
        {
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreateDeckResponse>(It.IsAny<Deck>()))
                .Returns(new CreateDeckResponse());
            var deckRepositoryMock = new Mock<IDeckRepository>();
            var deckCardsInterractorMock = new Mock<IDeckCardsInterractor>();
            deckCardsInterractorMock.Setup(x => x.Create(52)).Returns(new Deck{
                Cards = new List<Card>()
            });
            var controller = new DeckOperationsController(deckRepositoryMock.Object, mapperMock.Object, deckCardsInterractorMock.Object);

            var actionResult = controller.CreateDeck(52).Result as OkObjectResult;

            var createDeckResponse = (CreateDeckResponse)actionResult.Value;
            Assert.NotNull(createDeckResponse);
        }

        [Test]
        [TestCase("hash")]
        public void GetDeck_ReturnsDeckWithGivenHash_IfItExists(string hash)
        {
            var mapperMock = new Mock<IMapper>();
            var deckCardsInterractorMock = new Mock<IDeckCardsInterractor>();
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(hash)).Returns(new Deck{
                Hash = hash
            });
            
            var controller = new DeckOperationsController(deckRepositoryMock.Object, mapperMock.Object, deckCardsInterractorMock.Object);
            var actionResult = controller.GetDeck(hash).Result as OkObjectResult;
            var deck = (Deck)actionResult.Value;

            Assert.NotNull(deck);
            Assert.That(deck.Hash == hash);
        }
        
        [Test]
        [TestCase("hash")]
        public void Shuffle_CallsInterractor_AndReturnsOk(string hash)
        {
            var mapperMock = new Mock<IMapper>();
            var deckCardsInterractorMock = new Mock<IDeckCardsInterractor>();
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(hash)).Returns(new Deck{
                Hash = hash
            });
            
            var controller = new DeckOperationsController(deckRepositoryMock.Object, mapperMock.Object, deckCardsInterractorMock.Object);
            var result = controller.ShuffleDeck(hash);

            deckCardsInterractorMock.Verify(x => x.Shuffle(hash));
            Assert.IsInstanceOf<OkResult>(result);
        }

        [Test]
        [TestCase("hash", CollectionDirection.Top, 2)]
        [TestCase("hash", CollectionDirection.Bottom, 3)]
        public void Draw_CallsInterractor(string hash, CollectionDirection direction, int count)
        {
            var mapperMock = new Mock<IMapper>();
            var deckCardsInterractorMock = new Mock<IDeckCardsInterractor>();
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(hash)).Returns(new Deck{
                Hash = hash
            });

            var controller = new DeckOperationsController(deckRepositoryMock.Object, mapperMock.Object, deckCardsInterractorMock.Object);
            var request = new DeckDrawRequest{
                Hash = hash,
                Direction = direction,
                Count = count
            };
            var result = controller.Draw(request);
            
            Assert.IsInstanceOf<OkObjectResult>(result);
            deckCardsInterractorMock.Verify(x => x.Draw(direction,hash,count));
        }

        [Test]
        [TestCase("hash", CollectionDirection.Bottom)]
        [TestCase("hash", CollectionDirection.Top)]
        public void Insert_CallsInterractor(string hash, CollectionDirection direction)
        {
            var mapperMock = new Mock<IMapper>();
            var deckCardsInterractorMock = new Mock<IDeckCardsInterractor>();
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(hash)).Returns(new Deck{
                Hash = hash
            });

            var controller = new DeckOperationsController(deckRepositoryMock.Object, mapperMock.Object, deckCardsInterractorMock.Object);
            var request = new DeckCardInsertRequest{
                Hash = hash,
                Direction = direction,
                Card = It.IsAny<Card>()
            };
            var result = controller.Insert(request);
            
            Assert.IsInstanceOf<OkResult>(result);
            deckCardsInterractorMock.Verify(x => x.Insert(hash,direction,It.IsAny<Card>()));
        }
    }
}