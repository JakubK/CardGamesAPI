using CardGamesAPI.Models;
using CardGamesAPI.Services;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class CardsFactoryTests
    {
        [Test]
        public void Generate_WhenNoParamsGiven_ReturnsFullPreset()
        {
            var factory = new CardsFactory();
            var cards = factory.Generate();
            Assert.That(cards.Count == 52);
        }

        [Test]
        [TestCase(1)]
        [TestCase(10)]
        [TestCase(51)]
        [TestCase(52)]
        public void Generate_WhenParamGiven_ReturnsSubsetOfGivenLength(int length)
        {
            var factory = new CardsFactory();
            var cards = factory.Generate(length);
            Assert.That(cards.Count == length);
        }

        [Test]
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-2)]
        public void Generate_WhenNonPositiveParamGiven_ReturnsEmptySet(int length)
        {
            var factory = new CardsFactory();
            var cards = factory.Generate(length);
            Assert.That(cards.Count == 0);
        }

        [Test]
        [TestCase(53)]
        [TestCase(54)]
        public void Generate_WhenParamGreaterThanFullPresetSize_ReturnsEmptySet(int length)
        {
            var factory = new CardsFactory();
            var cards = factory.Generate(length);
            Assert.That(cards.Count == 0);
        }

        [Test]
        [TestCase(0, Color.Spades)]
        [TestCase(12, Color.Spades)]

        [TestCase(13,Color.Hearts)]
        [TestCase(25,Color.Hearts)]

        [TestCase(26,Color.Clubs)]
        [TestCase(38,Color.Clubs)]

        [TestCase(39,Color.Diamonds)]
        [TestCase(51,Color.Diamonds)]
        public void Generate_AssignsColorProperly(int index, Color desiredColor)
        {
            var factory = new CardsFactory();
            var cards = factory.Generate();
            Assert.That(cards[index].Color == desiredColor);
        }
    }
}