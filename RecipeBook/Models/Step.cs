using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecipeBook.Models
{
    public class Step
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }

        public int RecipeId { get; set; }
        
        [ForeignKey("RecipeId")]
        public Recipe Recipe { get; set; }
    }
}
