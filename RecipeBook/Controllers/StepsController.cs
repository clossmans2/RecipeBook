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
    public class StepsController : ControllerBase
    {
        private readonly RecipeBookContext _context;

        public StepsController(RecipeBookContext context)
        {
            _context = context;
        }

        // GET: api/Steps
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StepDTO>>> GetSteps()
        {
          if (_context.Steps == null)
          {
              return NotFound();
          }
            var steps = await _context.Steps.ToListAsync();
            var stepsDTO = new List<StepDTO>();
            foreach (var step in steps)
            {
                stepsDTO.Add(StepDTO.GetStepDTO(step));
            }
            return stepsDTO;
        }

        // GET: api/Steps/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StepDTO>> GetStep(int id)
        {
          if (_context.Steps == null)
          {
              return NotFound();
          }
            var step = await _context.Steps.FindAsync(id);

            if (step == null)
            {
                return NotFound();
            }

            return StepDTO.GetStepDTO(step);
        }

        // PUT: api/Steps/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStep(int id, StepDTO stepDto)
        {
            if (id != stepDto.Id)
            {
                return BadRequest();
            }
            var step = await _context.Steps.FindAsync(id);
            step.Text = stepDto.Text;
            step.Order = stepDto.Order;

            //_context.Entry(step).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StepExists(id))
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

        // POST: api/Steps
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StepDTO>> PostStep(StepDTO stepDto)
        {
            if (_context.Steps == null)
            {
                return Problem("Entity set 'RecipeBookContext.Steps'  is null.");
            }
            var step = new Step
            {
                Text = stepDto.Text,
                Order = stepDto.Order
            };
            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStep", new { id = step.Id }, stepDto);
        }

        // POST: api/Steps/1
        [HttpPost("{recipeId}")]
        public async Task<ActionResult<StepDTO>> PostStepToRecipe(int recipeId, StepDTO stepDto)
        {
            if (_context.Steps == null)
            {
                return Problem("Entity set 'RecipeBookContext.Steps'  is null.");
            }
            var recipe = await _context.Recipes.FindAsync(recipeId);
            var step = new Step
            {
                Text = stepDto.Text,
                Order = stepDto.Order,
                Recipe = recipe,
                RecipeId = recipe.Id
            };
            _context.Steps.Add(step);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStep", new { id = step.Id }, stepDto);
        }

        // DELETE: api/Steps/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStep(int id)
        {
            if (_context.Steps == null)
            {
                return NotFound();
            }
            var step = await _context.Steps.FindAsync(id);
            if (step == null)
            {
                return NotFound();
            }

            _context.Steps.Remove(step);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StepExists(int id)
        {
            return (_context.Steps?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
