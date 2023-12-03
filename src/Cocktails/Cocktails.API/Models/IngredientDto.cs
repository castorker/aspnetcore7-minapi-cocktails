namespace Cocktails.API.Models
{
    public class IngredientDto
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public int CocktailId { get; set; }
    }
}
