using System;
using System.Collections.Generic;
using AutoMapper;
using CardGamesAPI.Contracts.Requests;
using CardGamesAPI.Contracts.Responses;
using CardGamesAPI.Models;
using CardGamesAPI.Repositories;
using CardGamesAPI.Services;
using HashidsNet;
using Microsoft.AspNetCore.Mvc;

namespace CardGamesAPI.Controllers
{   
    [Route("api/piles")]
    [ApiController]
    public class PileOperationsController : ControllerBase
    {
        IPileCardsInterractor _pileCardsInterractor;
        IMapper _mapper;
        IDeckRepository _deckRepository;
        IHashids _hashids;
        public PileOperationsController(IPileCardsInterractor pileCardsInterractor,
            IMapper mapper,
            IDeckRepository deckRepository,
            IHashids hashids)
        {
            _pileCardsInterractor = pileCardsInterractor;
            _mapper = mapper;
            _deckRepository = deckRepository;
            _hashids = hashids;
        }

        [HttpPost]
        public ActionResult<CreatePileResponse> CreatePile(string deckHash)
        {
            var deck = _deckRepository.GetDeck(deckHash);
            var pile = new Pile{
                Hash = _hashids.Encode(deck.Piles.Count)
            };
            deck.Piles.Add(pile);
            _deckRepository.Update(deck);
            return Ok(_mapper.Map<CreatePileResponse>(pile));
        }

        public ActionResult Shuffle(string deckHash, string pileHash)
        {
            _pileCardsInterractor.Shuffle(deckHash,pileHash);
            return Ok();
        }

        public ActionResult<List<Card>> Draw(PileDrawRequest pileDrawRequest)
        {
            var cards = _pileCardsInterractor.Draw(pileDrawRequest.Direction,
                pileDrawRequest.DeckHash,
                pileDrawRequest.PileHash,
                pileDrawRequest.Count);

            return Ok(cards);
        }
    }
}