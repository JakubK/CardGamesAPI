using System.Collections.Generic;
using LiteDB;

namespace CardGamesAPI.Models
{
    public class Deck 
    {
      [BsonId]
      public int Id {get;set;}
      public string Hash {get;set;}
      public int Remaining {get;set;}
      public List<Card> Cards {get;set;}
      public Pile[] Piles {get;set;}
    }
}