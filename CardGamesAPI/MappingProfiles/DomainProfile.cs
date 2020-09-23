using AutoMapper;
using CardGamesAPI.Contracts.Responses;
using CardGamesAPI.Models;

namespace CardGamesAPI.MappingProfiles
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            CreateMap<Deck,CreateDeckResponse>();
            CreateMap<Pile,CreatePileResponse>();
        }
    }
}