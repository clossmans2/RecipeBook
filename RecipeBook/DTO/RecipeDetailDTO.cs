using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.Models;

namespace RecipeBook.DTO
{
    public class RecipeDetailDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public virtual ICollection<StepDTO> Steps { get; set; } = new List<StepDTO>();
        public virtual ICollection<IngredientDTO> Ingredients { get; set; } = new List<IngredientDTO>();
        public AuthorDTO Author { get; set; }


        public static RecipeDetailDTO GetRecipeDetail(Recipe recipe)
        {
            var dto = new RecipeDetailDTO
            {
                Id = recipe.Id,
                Name = recipe.Name,
                Description = recipe.Description,
                PrepTime = recipe.PrepTime,
                Author = AuthorDTO.GetAuthorDTO(recipe.Author)
            };
            if (recipe.Steps != null)
            {
                foreach (var step in recipe.Steps)
                {
                    dto.Steps.Add(StepDTO.GetStepDTO(step));
                }
            }
            else
            {
                dto.Steps = new List<StepDTO>();
            }

            if (recipe.Ingredients != null)
            {
                foreach (var ingredient in recipe.Ingredients)
                {
                    dto.Ingredients.Add(IngredientDTO.GetIngredientDTO(ingredient));
                }
            }
            else
            {
                dto.Ingredients = new List<IngredientDTO>();
            }
           
            return dto;
        }
    }
}
