using System.Collections.Generic;
using CardGamesAPI.Models;

namespace CardGamesAPI.Services
{
    public interface ICardsFactory
    {
        List<Card> Generate(int size);
    }
}