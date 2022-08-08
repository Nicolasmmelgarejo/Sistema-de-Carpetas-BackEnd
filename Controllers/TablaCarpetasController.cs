using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using backend.data;
using backend.models;

namespace backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TablaCarpetasController : ControllerBase
    {
        private readonly DataContext _context;

        public TablaCarpetasController(DataContext context)
        {
            _context = context;
        }

        // GET: api/TablaCarpetas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TablaCarpetas>>> GettablaCarpetas()
        {
          if (_context.tablaCarpetas == null)
          {
              return NotFound();
          }
            return await _context.tablaCarpetas.ToListAsync();
        }

        // GET: api/TablaCarpetas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TablaCarpetas>> GetTablaCarpetas(int id)
        {
          if (_context.tablaCarpetas == null)
          {
              return NotFound();
          }
            var tablaCarpetas = await _context.tablaCarpetas.FindAsync(id);

            if (tablaCarpetas == null)
            {
                return NotFound();
            }

            return tablaCarpetas;
        }

        // PUT: api/TablaCarpetas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTablaCarpetas(int id, TablaCarpetas tablaCarpetas)
        {
            if (id != tablaCarpetas.Id)
            {
                return BadRequest();
            }

            _context.Entry(tablaCarpetas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TablaCarpetasExists(id))
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

        // POST: api/TablaCarpetas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<TablaCarpetas>> PostTablaCarpetas(TablaCarpetas tablaCarpetas)
        {
          if (_context.tablaCarpetas == null)
          {
              return Problem("Entity set 'DataContext.tablaCarpetas'  is null.");
          }
            _context.tablaCarpetas.Add(tablaCarpetas);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTablaCarpetas", new { id = tablaCarpetas.Id }, tablaCarpetas);
        }

        // DELETE: api/TablaCarpetas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTablaCarpetas(int id)
        {
            if (_context.tablaCarpetas == null)
            {
                return NotFound();
            }
            var tablaCarpetas = await _context.tablaCarpetas.FindAsync(id);
            if (tablaCarpetas == null)
            {
                return NotFound();
            }

            _context.tablaCarpetas.Remove(tablaCarpetas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TablaCarpetasExists(int id)
        {
            return (_context.tablaCarpetas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
