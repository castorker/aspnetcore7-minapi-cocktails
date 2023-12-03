using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Cocktails.API.Entities
{
    public class Cocktail
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public required string Name { get; set; }

        public ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();

        public Cocktail()
        {
        }

        [SetsRequiredMembers]
        public Cocktail(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
