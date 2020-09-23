using AutoMapper;
using CardGamesAPI.Contracts.Requests;
using CardGamesAPI.Contracts.Responses;
using CardGamesAPI.Controllers;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class PileOperationsControllerTests
    {
        [Test]
        [TestCase("hash")]
        public void CreatePile_ReturnsPileHandle(string hash)
        {
            var pileCardsInterractorMock = new Mock<IPileCardsInterractor>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreatePileResponse>(It.IsAny<Pile>()))
                .Returns(new CreatePileResponse());
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(hash)).Returns(new Deck());
            var hashidsMock = new Mock<IHashids>();
            var controller = new PileOperationsController(pileCardsInterractorMock.Object, mapperMock.Object, repositoryMock.Object, hashidsMock.Object);

            var response = controller.CreatePile(hash).Result as OkObjectResult;
            var createDeckResponse = (CreatePileResponse)response.Value;

            Assert.NotNull(createDeckResponse);
        }

        public void GetPile_ReturnsPileWithGivenHash_IfItExists(string hash)
        {
            
        }
        
        [Test]
        [TestCase("deckHash", "pileHash")]
        public void Shuffle_CallsInterractor_AndReturnsOk(string deckHash, string pileHash)
        {
            var pileCardsInterractorMock = new Mock<IPileCardsInterractor>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreatePileResponse>(It.IsAny<Pile>()))
                .Returns(new CreatePileResponse());
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(deckHash)).Returns(new Deck());
            var hashidsMock = new Mock<IHashids>();
            var controller = new PileOperationsController(pileCardsInterractorMock.Object, mapperMock.Object, repositoryMock.Object, hashidsMock.Object);

            var response = controller.Shuffle(deckHash, pileHash) as OkResult;

            Assert.NotNull(response);
            pileCardsInterractorMock.Verify(x => x.Shuffle(deckHash, pileHash));
        }
        
        [Test]
        [TestCase(1, CollectionDirection.Bottom)]
        [TestCase(2, CollectionDirection.Top)]
        public void Draw_CallsInterractor(int count, CollectionDirection direction)
        {
            PileDrawRequest pileDrawRequest = new PileDrawRequest{
                DeckHash = "deck",
                PileHash = "pile",
                Count = count,
                Direction = direction
            };
            var pileCardsInterractorMock = new Mock<IPileCardsInterractor>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreatePileResponse>(It.IsAny<Pile>()))
                .Returns(new CreatePileResponse());
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(pileDrawRequest.DeckHash)).Returns(new Deck());
            var hashidsMock = new Mock<IHashids>();
            var controller = new PileOperationsController(pileCardsInterractorMock.Object, mapperMock.Object, repositoryMock.Object, hashidsMock.Object);

            var response = controller.Draw(pileDrawRequest).Result as OkObjectResult;

            Assert.NotNull(response);
            pileCardsInterractorMock.Verify(x => x.Draw(pileDrawRequest.Direction,
                pileDrawRequest.DeckHash,
                pileDrawRequest.PileHash,
                pileDrawRequest.Count));
        }

        public void Insert_CallsInterractor(string hash, CollectionDirection direction)
        {

        }
    }
}