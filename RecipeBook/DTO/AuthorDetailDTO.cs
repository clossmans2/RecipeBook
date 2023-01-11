using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.Models;

namespace RecipeBook.DTO
{
    public class AuthorDetailDTO
    {
        public int Id { get; set; }
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        public ICollection<RecipeDTO> Recipes { get; set; } = new List<RecipeDTO>();

        public static AuthorDetailDTO GetAuthorDetail(Author author, IEnumerable<Recipe> recipes)
        {
            var recipeDTOList = new List<RecipeDTO>();
            foreach (var recipe in recipes)
            {
                recipeDTOList.Add(RecipeDTO.GetRecipeDTO(recipe));
            }

            return new AuthorDetailDTO
            {
                Id = author.Id,
                FirstName = author.FirstName,
                LastName = author.LastName,
                Email = author.Email,
                Recipes = recipeDTOList
            };
        }
    }
}
