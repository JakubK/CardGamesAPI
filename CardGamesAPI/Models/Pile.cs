using System.Collections.Generic;

namespace CardGamesAPI.Models
{
    public class Pile
    {
        public string Hash {get;set;}
        public List<Card> Cards {get;set;} = new List<Card>();
    }
}