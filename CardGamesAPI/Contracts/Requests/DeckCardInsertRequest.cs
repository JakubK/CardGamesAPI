using CardGamesAPI.Models;

namespace CardGamesAPI.Contracts.Requests
{
    public class DeckCardInsertRequest
    {
        public string Hash {get;set;}
        public CollectionDirection Direction {get;set;}
        public Card Card {get;set;}
    }
}