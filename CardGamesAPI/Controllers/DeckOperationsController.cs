using AutoMapper;
using CardGamesAPI.Contracts.Requests;
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

            var response =  _mapper.Map<CreateDeckResponse>(deck);
            response.Remaining = deck.Cards.Count;

            return Ok(response);
        }

        [HttpPut]
        public ActionResult Insert([FromBody]DeckCardInsertRequest request)
        {
            _deckCardsInterractor.Insert(request.Hash, request.Direction, request.Card);
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

        [HttpPut("draw")]
        public ActionResult Draw([FromBody]DeckDrawRequest request)
        {
            return Ok(_deckCardsInterractor.Draw(request.Direction,request.Hash,request.Count));
        }
    } 
}