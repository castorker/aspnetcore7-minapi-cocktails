using AutoMapper;
using Cocktails.API.Entities;
using Cocktails.API.Models;

namespace Cocktails.API.Profiles
{
    public class IngredientProfile : Profile
    {
        public IngredientProfile()
        {
            CreateMap<Ingredient, IngredientDto>()
                .ForMember(
                    c => c.CocktailId,
                    o => o.MapFrom(s => s.Cocktails.First().Id));
        }
    }
}
