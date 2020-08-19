using LiteDB;

namespace CardGamesAPI.Models
{
    public class Deck 
    {
      [BsonId]
      public string Hash {get;set;}
      public int Remaining {get;set;}
      public Card[] Cards {get;set;}
      public Pile[] Piles {get;set;}
    }
}