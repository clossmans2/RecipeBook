using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecipeBook.Models;

namespace RecipeBook.DTO
{
    public class StepDTO
    {
        public int Id { get; set; }
        public string Text { get; set; } = string.Empty;
        public int Order { get; set; }

        public static StepDTO GetStepDTO(Step step)
        {
            return new StepDTO
            {
                Id = step.Id,
                Text = step.Text,
                Order = step.Order
            };
        }
    }
}
