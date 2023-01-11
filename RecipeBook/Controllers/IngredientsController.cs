using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RecipeBook.Data;
using RecipeBook.DTO;
using RecipeBook.Models;

namespace RecipeBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngredientsController : ControllerBase
    {
        private readonly RecipeBookContext _context;

        public IngredientsController(RecipeBookContext context)
        {
            _context = context;
        }

        // GET: api/Ingredients
        [HttpGet]
        public async Task<ActionResult<IEnumerable<IngredientDTO>>> GetIngredients()
        {
          if (_context.Ingredients == null)
          {
              return NotFound();
          }
            var ingreds = await _context.Ingredients.ToListAsync();
            var ingredDTOs = new List<IngredientDTO>();
            foreach (var ingred in ingreds)
            {
                var iDto = IngredientDTO.GetIngredientDTO(ingred);
                ingredDTOs.Add(iDto);
            }

            return ingredDTOs;
        }

        // GET: api/Ingredients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IngredientDTO>> GetIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _context.Ingredients.FindAsync(id);

            if (ingredient == null)
            {
                return NotFound();
            }

            var iDto = IngredientDTO.GetIngredientDTO(ingredient);

            return iDto;
        }

        // PUT: api/Ingredients/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutIngredient(int id, IngredientDTO ingredient)
        {
            if (id != ingredient.Id)
            {
                return BadRequest();
            }
            var ingred = new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity
            };
            
            _context.Entry(ingred).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!IngredientExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Ingredients
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<IngredientDTO>> PostIngredient(IngredientDTO ingredient)
        {
            if (_context.Ingredients == null)
            {
                return Problem("Entity set 'RecipeBookContext.Ingredients'  is null.");
            }
            var ingred = new Ingredient
            {
                Id = ingredient.Id,
                Name = ingredient.Name,
                Quantity = ingredient.Quantity
            };
            _context.Ingredients.Add(ingred);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetIngredient", new { id = ingredient.Id }, ingredient);
        }

        // DELETE: api/Ingredients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteIngredient(int id)
        {
            if (_context.Ingredients == null)
            {
                return NotFound();
            }
            var ingredient = await _context.Ingredients.FindAsync(id);
            if (ingredient == null)
            {
                return NotFound();
            }

            _context.Ingredients.Remove(ingredient);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool IngredientExists(int id)
        {
            return (_context.Ingredients?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
