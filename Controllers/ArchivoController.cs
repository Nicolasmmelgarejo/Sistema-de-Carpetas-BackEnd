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
    public class ArchivoController : ControllerBase
    {
        private readonly DataContext _context;

        public ArchivoController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Archivo
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Archivo>>> Getarchivo()
        {
          if (_context.archivo == null)
          {
              return NotFound();
          }
            return await _context.archivo.ToListAsync();
        }

        // GET: api/Archivo/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Archivo>> GetArchivo(int id)
        {
          if (_context.archivo == null)
          {
              return NotFound();
          }
            var archivo = await _context.archivo.FindAsync(id);

            if (archivo == null)
            {
                return NotFound();
            }

            return archivo;
        }

        // PUT: api/Archivo/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArchivo(int id, Archivo archivo)
        {
            if (id != archivo.Id)
            {
                return BadRequest();
            }

            _context.Entry(archivo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArchivoExists(id))
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

        // POST: api/Archivo
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Archivo>> PostArchivo(Archivo archivo)
        {
          if (_context.archivo == null)
          {
              return Problem("Entity set 'DataContext.archivo'  is null.");
          }
            _context.archivo.Add(archivo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArchivo", new { id = archivo.Id }, archivo);
        }

        // DELETE: api/Archivo/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArchivo(int id)
        {
            if (_context.archivo == null)
            {
                return NotFound();
            }
            var archivo = await _context.archivo.FindAsync(id);
            if (archivo == null)
            {
                return NotFound();
            }

            _context.archivo.Remove(archivo);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArchivoExists(int id)
        {
            return (_context.archivo?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
