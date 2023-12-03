using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cocktails.API.Entities
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public ICollection<Cocktail> Cocktails { get; set; } = new List<Cocktail>();

        public Ingredient()
        { }

        [SetsRequiredMembers]
        public Ingredient(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
