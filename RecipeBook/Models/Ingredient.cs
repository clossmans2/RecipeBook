using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class Ingredient
    {
        public int Id { get; set; }
        public string Quantity { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;

        public virtual ICollection<Recipe> Recipes { get; set; } = new List<Recipe>();
    }
}
