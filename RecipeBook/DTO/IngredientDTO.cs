using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.Models;

namespace RecipeBook.DTO
{
    public class IngredientDTO
    {
        public int Id { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public static IngredientDTO GetIngredientDTO(Ingredient ingredient)
        {
            return new IngredientDTO
            {
                Id = ingredient.Id,
                Quantity = ingredient.Quantity,
                Name = ingredient.Name
            };
        }
    }
}
