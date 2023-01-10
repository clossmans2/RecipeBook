using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class Recipe
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string PrepTime { get; set; } = string.Empty;
        public virtual ICollection<Step> Steps { get; set; } = new List<Step>();
        public virtual ICollection<Ingredient> Ingredients { get; set; } = new List<Ingredient>();
        public int AuthorId { get; set; }
        
        
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }

    }
}
