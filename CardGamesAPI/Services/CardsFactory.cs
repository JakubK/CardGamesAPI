using System.Collections.Generic;
using System.Linq;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
    public class CardsFactory : ICardsFactory
    {
        string[] presets;
        public CardsFactory()
        {
            presets = new string[] 
            {
                "2", "3","4","5","6", "7","8","9","10","J","Q","K","A",
                "2", "3","4","5","6", "7","8","9","10","J","Q","K","A",
                "2", "3","4","5","6", "7","8","9","10","J","Q","K","A",
                "2", "3","4","5","6", "7","8","9","10","J","Q","K","A",
            };
        }
        public List<Card> Generate(int size = 52)
        {
            if(size <= 0 || size > presets.Length)
                return new List<Card>();
            int i = 0;
            return presets.Select(x => new Card{
                Name = x,
                Color = (Color)(i++ % 4)
            })
            .Take(size)
            .ToList();
        }
    }
}