using System;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace CardGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        IDeckRepository _deckRepository;

        public OperationsController(IDeckRepository deckRepository)
        {
            _deckRepository = deckRepository;
        }

        [HttpPost]
        public ActionResult<Deck> CreateDeck()
        {
            var deck = new Deck();
            _deckRepository.Insert(deck);
            return Ok(deck);
        }

        [HttpGet]
        public ActionResult<Deck> GetDeck(string hash)
        {
            return Ok(_deckRepository.GetDeck(hash));
        }
    }
}