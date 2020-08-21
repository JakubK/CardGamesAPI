using LiteDB;

namespace CardGamesAPI.Models
{
    public class Pile
    {
        public string Name {get;set;}
        public Card[] Cards {get;set;}
    }
}