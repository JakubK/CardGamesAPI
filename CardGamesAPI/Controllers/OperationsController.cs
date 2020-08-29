using System;
using AutoMapper;
using CardGamesAPI.Contracts.Responses;
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
        IMapper _mapper;

        public OperationsController(IDeckRepository deckRepository, IMapper mapper)
        {
            _deckRepository = deckRepository;
            _mapper = mapper;
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
    }
}