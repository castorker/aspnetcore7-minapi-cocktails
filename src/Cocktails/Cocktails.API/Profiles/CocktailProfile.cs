using AutoMapper;
using Cocktails.API.Entities;
using Cocktails.API.Models;

namespace Cocktails.API.Profiles
{
    public class CocktailProfile : Profile
    {
        public CocktailProfile()
        {
            CreateMap<Cocktail, CocktailDto>();
        }        
    }
}
