using CardGamesAPI.Models;

namespace CardGamesAPI.Contracts.Requests
{
    public class PileCardInsertRequest
    {
        public string DeckHash {get;set;}
        public string PileHash {get;set;}
        public CollectionDirection Direction {get;set;}
        public Card Card {get;set;}
    }
}