using CardGamesAPI.Controllers;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class OperationsControllerTests
    {
        [Test]
        public void CreateDeck_ReturnsDeck()
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();
            var controller = new OperationsController(deckRepositoryMock.Object);
            var actionResult = controller.CreateDeck().Result as OkObjectResult;

            var deck = (Deck)actionResult.Value;
            Assert.NotNull(deck);
        }

        [Test]
        [TestCase("hash")]
        public void GetDeck_ReturnsDeckWithGivenHash_IfItExists(string hash)
        {
            var deckRepositoryMock = new Mock<IDeckRepository>();
            deckRepositoryMock.Setup(x => x.GetDeck(hash)).Returns(new Deck{
                Hash = hash
            });
            
            var controller = new OperationsController(deckRepositoryMock.Object);
            var actionResult = controller.GetDeck(hash).Result as OkObjectResult;
            var deck = (Deck)actionResult.Value;

            Assert.NotNull(deck);
            Assert.That(deck.Hash == hash);
        }
    }
}