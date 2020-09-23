using CardGamesAPI.Models;

namespace CardGamesAPI.Contracts.Requests
{
    public class PileDrawRequest
    {
        public string DeckHash {get;set;}
        public string PileHash {get;set;}
        public CollectionDirection Direction {get;set;}
        public int Count {get;set;}
    }
}