using CardGamesAPI.Models;

namespace CardGamesAPI.Contracts.Requests
{
    public class DeckDrawRequest
    {
        public string Hash {get;set;}
        public CollectionDirection Direction {get;set;}
        public int Count {get;set;}
    }
}