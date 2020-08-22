using System.Collections.Generic;
using CardGamesAPI.Models;
using CardGamesAPI.Services;
using NUnit.Framework;

namespace CardGamesAPI.Tests
{
    public class CardsHelperTests
    {
        [Test]
        public void Draw_Top_ReturnsProperCards()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Top,cards, 1);
            Assert.That(afterDraw.Count == 1);
            Assert.That(afterDraw[0].Name == "X");
        }

        [Test]
        public void Draw_Top_MutatesGivenCollection()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Top,cards, 1);
            Assert.That(cards.Count == 2);
        }

        [Test]
        public void Draw_Top_WhenNoParamGiven_DrawsOne()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Top,cards);
            Assert.That(afterDraw.Count == 1);
            Assert.That(afterDraw[0].Name == "X");
        }

        [Test]
        public void Draw_Top_WhenCountIsMoreThanAvailable_ReturnsSubset()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Top,cards, 4);
            Assert.That(afterDraw.Count == 3);
        }

        //FromBottom
        [Test]
        public void Draw_Bottom_ReturnsProperCards()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Bottom,cards, 1);
            Assert.That(afterDraw.Count == 1);
            Assert.That(afterDraw[0].Name == "Z");
        }

        [Test]
        public void Draw_Bottom_MutatesGivenCollection()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Bottom,cards, 1);
            Assert.That(cards.Count == 2);
        }

        [Test]
        public void Draw_Bottom_WhenNoParamGiven_DrawsOne()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Bottom,cards);
            Assert.That(afterDraw.Count == 1);
            Assert.That(afterDraw[0].Name == "Z");
        }

        [Test]
        public void Draw_Bottom_WhenCountIsMoreThanAvailable_ReturnsSubset()
        {
            var cardsHelper = new CardsHelper();
            var cards = new List<Card>()
            {
                new Card{Name= "X"},
                new Card{Name= "Y"},
                new Card{Name= "Z"}
            };

            var afterDraw = cardsHelper.Draw(CollectionDirection.Top,cards, 4);
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