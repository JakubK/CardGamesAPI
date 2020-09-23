using System.Collections.Generic;
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

        [Test]
        public void GetPile_ReturnsPileWithGivenHash_IfItExists()
        {
            var pileCardsInterractorMock = new Mock<IPileCardsInterractor>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreatePileResponse>(It.IsAny<Pile>()))
                .Returns(new CreatePileResponse());
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(It.IsAny<string>())).Returns(new Deck{
                Piles = new List<Pile>{
                    new Pile{Hash = "pileHash"}
                }
            });
            var hashidsMock = new Mock<IHashids>();
            var controller = new PileOperationsController(pileCardsInterractorMock.Object, mapperMock.Object, repositoryMock.Object, hashidsMock.Object);

            var response = controller.GetPile("deckHash", "pileHash").Result as OkObjectResult;
            var pile = (Pile)response.Value;

            Assert.NotNull(pile);
            Assert.That(pile.Hash == "pileHash");
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
        
        [Test]
        [TestCase("deckHash", CollectionDirection.Top)]
        public void Insert_CallsInterractor(string hash, CollectionDirection direction)
        {
            PileCardInsertRequest request = new PileCardInsertRequest{
                DeckHash = hash,
                Direction = direction,
                PileHash = It.IsAny<string>(),
                Card = It.IsAny<Card>()
            };
            var pileCardsInterractorMock = new Mock<IPileCardsInterractor>();
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(x => x.Map<CreatePileResponse>(It.IsAny<Pile>()))
                .Returns(new CreatePileResponse());
            var repositoryMock = new Mock<IDeckRepository>();
            repositoryMock.Setup(x => x.GetDeck(request.DeckHash)).Returns(new Deck());
            var hashidsMock = new Mock<IHashids>();
            var controller = new PileOperationsController(pileCardsInterractorMock.Object, mapperMock.Object, repositoryMock.Object, hashidsMock.Object);

            var response = controller.Insert(request) as OkResult;

            Assert.NotNull(response);
            pileCardsInterractorMock.Verify(x => x.Insert(request.DeckHash, request.PileHash, request.Direction, request.Card));
        }
    }
}