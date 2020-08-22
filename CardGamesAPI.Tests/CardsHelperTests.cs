using System.Collections.Generic;
using CardGamesAPI.Models;
using CardGamesAPI.Services;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class CardsHelperTests
    {
        [Test]
        [TestCase(CollectionDirection.Top, "X")]
        [TestCase(CollectionDirection.Bottom, "Z")]
        public void Draw_TopBottom_ReturnsProperCards(CollectionDirection direction, string expectedName)
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(direction,cards, 1);
            Assert.That(afterDraw.Count == 1);
            Assert.That(afterDraw[0].Name == expectedName);
        }

        [Test]
        [TestCase(CollectionDirection.Bottom)]
        [TestCase(CollectionDirection.Top)]
        public void Draw_TopBottom_MutatesGivenCollection(CollectionDirection direction)
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(direction,cards, 1);
            Assert.That(cards.Count == 2);
        }

        [Test]
        [TestCase(CollectionDirection.Bottom)]
        [TestCase(CollectionDirection.Top)]
        public void Draw_TopBottom_WhenNoParamGiven_DrawsOne(CollectionDirection direction)
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(direction,cards);
            Assert.That(afterDraw.Count == 1);
        }

        [Test]
        [TestCase(CollectionDirection.Bottom)]
        [TestCase(CollectionDirection.Top)]
        public void Draw_TopBottom_WhenCountIsMoreThanAvailable_ReturnsEverything(CollectionDirection direction)
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(direction,cards, 4);
            Assert.That(afterDraw.Count == 3);
        }

        [Test]
        public void Insert_Top_InsertsProperly()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };
            var cardToInsert = new Card { Name = "A" };
            cardsHelper.Insert(CollectionDirection.Top,cards,cardToInsert);

            Assert.That(cards.Count == 4);
            Assert.That(cards[0].Name == "A");
        }

        [Test]
        public void Insert_Bottom_InsertsProperly()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };
            var cardToInsert = new Card { Name = "A" };
            cardsHelper.Insert(CollectionDirection.Bottom,cards,cardToInsert);

            Assert.That(cards.Count == 4);
            Assert.That(cards[3].Name == "A");
        }

        [Test]
        public void Shuffle_ShufflesCollection()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>();
            for(int i = 0;i < 100;i++)
                cards.Add(new Card{ Name = i.ToString()});
            cardsHelper.Shuffle(ref cards);

            int j = 0;
            for(int i = 0;i < 100;i++)
                if(i.ToString() == cards[i].Name)
                    j++;

            Assert.That(j < 100);
            
        }
    }
}