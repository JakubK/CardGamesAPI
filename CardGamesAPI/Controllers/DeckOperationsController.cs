using AutoMapper;
using CardGamesAPI.Contracts.Responses;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace CardGamesAPI.Controllers
{
    [Route("api/deck")]
    [ApiController]
    public class DeckOperationsController : ControllerBase
    {
        IDeckRepository _deckRepository;
        IMapper _mapper;
        IDeckCardsInterractor _deckCardsInterractor;

        public DeckOperationsController(
            IDeckRepository deckRepository,
            IMapper mapper,
            IDeckCardsInterractor deckCardsInterractor)
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
            _deckCardsInterractor = deckCardsInterractor;
        }

        [HttpPost("{count}")]
        public ActionResult<CreateDeckResponse> CreateDeck(int count)
        {
            var deck = _deckCardsInterractor.Create(count);
            _deckRepository.Insert(deck);
            return Ok(_mapper.Map<CreateDeckResponse>(deck));
        }

        [HttpPut]
        public ActionResult Insert(string hash, CollectionDirection direction, Card card)
        {
            _deckCardsInterractor.Insert(hash, direction, card);
            return Ok();
        }

        [HttpGet("{hash}")]
        public ActionResult<Deck> GetDeck([FromRoute]string hash)
        {
            return Ok(_deckRepository.GetDeck(hash));
        }

        [HttpPut("shuffle/{hash}")]
        public ActionResult ShuffleDeck([FromRoute]string hash)
        {
            _deckCardsInterractor.Shuffle(hash);
            return Ok();
        }

        [HttpPut("draw/{hash}/{direction}/{count}")]
        public ActionResult Draw(string hash, CollectionDirection direction, int count)
        {
            var cards = _deckCardsInterractor.Draw(direction,hash,count);
            return Ok(cards);
        }
    } 
}