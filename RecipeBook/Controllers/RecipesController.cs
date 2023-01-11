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
    public class RecipesController : ControllerBase
    {
        private readonly RecipeBookContext _context;

        public RecipesController(RecipeBookContext context)
        {
            _context = context;
        }

        // GET: api/Recipes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<RecipeDTO>>> GetRecipes()
        {
          if (_context.Recipes == null)
          {
              return NotFound();
          }
            var recipes = await _context.Recipes.ToListAsync();
            var recipeDtos = new List<RecipeDTO>();
            foreach (var recipe in recipes)
            {
                var dto = RecipeDTO.GetRecipeDTO(recipe);
                recipeDtos.Add(dto);
            }
            return recipeDtos;
        }

        // GET: api/Recipes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<RecipeDetailDTO>> GetRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes
                .Include(r => r.Steps)
                .Include(r => r.Ingredients)
                .Include(r => r.Author)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (recipe == null)
            {
                return NotFound();
            }

            var rDto = RecipeDTO.GetRecipeDTO(recipe);

            return Ok(rDto);
        }

        // PUT: api/Recipes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutRecipe(int id, RecipeDTO recipe)
        {
            if (id != recipe.Id)
            {
                return BadRequest();
            }
            var recipeToUpdate = await _context.Recipes.FindAsync(id);
            recipeToUpdate.Name = recipe.Name;
            recipeToUpdate.Description = recipe.Description;
            recipeToUpdate.PrepTime = recipe.PrepTime;

            //_context.Entry(recipeToUpdate).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        [HttpPut("{id}/Author/{authorId}")]
        public async Task<IActionResult> PutRecipeAuthor(int id, int authorId)
        {
            var recipe = await _context.Recipes.FindAsync(id);
            var author = await _context.Authors.FindAsync(authorId);

            if (recipe == null || author == null)
            {
                return NotFound();
            }

            recipe.Author = author;
            recipe.AuthorId = author.Id;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RecipeExists(id))
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

        // POST: api/Recipes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<RecipeDTO>> PostRecipe(RecipeDTO recipeDto)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'RecipeBookContext.Recipes'  is null.");
            }
            var recipe = new Recipe
            {
                Name = recipeDto.Name,
                Description = recipeDto.Description,
                PrepTime = recipeDto.PrepTime
            };

            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipeDto.Id }, recipeDto);
        }

        [HttpPost("Author/{authorId}")]
        public async Task<ActionResult<Recipe>> PostRecipeWithAuthor(int authorId, RecipeDTO recipeDto)
        {
            if (_context.Recipes == null)
            {
                return Problem("Entity set 'RecipeBookContext.Recipes'  is null.");
            }
            var author = await _context.Authors.FindAsync(authorId);
            if (author == null)
            {
                return NotFound();
            }
            var recipe = new Recipe
            {
                Name = recipeDto.Name,
                Description = recipeDto.Description,
                PrepTime = recipeDto.PrepTime,
                Author = author,
                AuthorId = author.Id
            };
            _context.Recipes.Add(recipe);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetRecipe", new { id = recipe.Id }, recipe);
        }

        // DELETE: api/Recipes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecipe(int id)
        {
            if (_context.Recipes == null)
            {
                return NotFound();
            }
            var recipe = await _context.Recipes.FindAsync(id);
            if (recipe == null)
            {
                return NotFound();
            }

            _context.Recipes.Remove(recipe);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool RecipeExists(int id)
        {
            return (_context.Recipes?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
