using System;
using AutoMapper;
using CardGamesAPI.Contracts.Responses;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardGamesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationsController : ControllerBase
    {
        IDeckRepository _deckRepository;
        IMapper _mapper;
        IDeckCardsInterractor _deckCardsInterractor;

        public OperationsController(
            IDeckRepository deckRepository,
            IMapper mapper,
            IDeckCardsInterractor deckCardsInterractor)
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
            _deckCardsInterractor = deckCardsInterractor;
        }

        [HttpPost]
        public ActionResult<CreateDeckResponse> CreateDeck()
        {
            var deck = new Deck();
            _deckRepository.Insert(deck);
            return Ok(_mapper.Map<CreateDeckResponse>(deck));
        }

        [HttpGet("{hash}")]
        public ActionResult<Deck> GetDeck([FromRoute]string hash)
        {
            return Ok(_deckRepository.GetDeck(hash));
        }

        [HttpPut("{hash}")]
        public ActionResult ShuffleDeck([FromRoute]string hash)
        {
            _deckCardsInterractor.Shuffle(hash);
            return Ok();
        }
        [HttpPut]
        public ActionResult Draw(string hash, CollectionDirection direction, int count)
        {
            var cards = _deckCardsInterractor.Draw(direction,hash,count);
            return Ok(cards);
        }
        
        [HttpPut]
        public ActionResult Insert(string hash, CollectionDirection direction, Card card)
        {
            _deckCardsInterractor.Insert(hash, direction, card);
            return Ok();
        }
    } 
}