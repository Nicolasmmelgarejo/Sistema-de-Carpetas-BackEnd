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
    public class CarpetaController : ControllerBase
    {
        private readonly DataContext _context;
        private TablaCarpetasController _controller;
        private ArchivoController archivoController;
        public CarpetaController(DataContext context)
        {
            _context = context;
        }

        // GET: api/Carpeta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Carpeta>>> Getcarpeta()
        {
          if (_context.carpeta == null)
          {
              return NotFound();
          }
            List<Carpeta> carpetaList = await _context.carpeta.ToListAsync();
            List<Archivo> archivosList = await _context.archivo.ToListAsync();
            for (int i = 0; i > carpetaList.Count; i++)
            {
                for (int j = 0; j < archivosList.Count; j++)
                {
                    if (carpetaList[i].Archivos == null)
                    {

                        carpetaList[i].Archivos = new List<Archivo>();
                    }
                    if (archivosList[j].CarpetaId == carpetaList[i].Id)
                    {
                        carpetaList[i].Archivos.Add(archivosList[j]);
                    }
                }
                
            }
            for (int i = 0; i < carpetaList.Count; i++)
            {
                for (int j = 0; j < archivosList.Count; j++)
                {
                    if (carpetaList[i].Archivos == null)
                    {

                        carpetaList[i].Archivos = new List<Archivo>();
                    }

                    if (archivosList[j].NombreArchivo == "vacio")
                    {
                        carpetaList[i].Archivos.Remove(archivosList[j]);
                    }
                }

            }
            
            List<TablaCarpetas> tablaCarpetasList = await _context.tablaCarpetas.ToListAsync();
            for (int i = 0; i < carpetaList.Count; i++)
            {
                for (int j = 0; j < tablaCarpetasList.Count; j++)
                {
                    if (tablaCarpetasList[j].NombreCarpeta == carpetaList[i].NombreCarpeta)
                    {
                        if (carpetaList[i].TablaCarpetas == null)
                        {

                            carpetaList[i].TablaCarpetas = new List<TablaCarpetas>();
                        }
                        carpetaList[i].TablaCarpetas.Add(tablaCarpetasList[j]);
                    }
                    else
                    {
                        if (carpetaList[i].TablaCarpetas != null)
                        {
                            carpetaList[i].TablaCarpetas.Remove(tablaCarpetasList[j]);

                        }
                    }
                }
            }
            return carpetaList;
        }

        // GET: api/Carpeta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Carpeta>> GetCarpeta(int id)
        {
          if (_context.carpeta == null)
          {
              return NotFound();
          }
            var carpeta = await _context.carpeta.FindAsync(id);

            if (carpeta == null)
            {
                return NotFound();
            }
            List<Archivo> archivosList = await _context.archivo.ToListAsync();
            for (int j = 0; j < archivosList.Count; j++)
            {
                if (archivosList[j].CarpetaId == carpeta.Id)
                {
                    carpeta.Archivos.Add(archivosList[j]);
                }
            }
            for (int j = 0; j < archivosList.Count; j++)
            {

                if (archivosList[j].NombreArchivo == "vacio")
                {
                    carpeta.Archivos.Remove(archivosList[j]);
                }
            }
            List<TablaCarpetas> tablaCarpetasList = await _context.tablaCarpetas.ToListAsync();
            for (int j = 0; j < tablaCarpetasList.Count; j++)
            {
                if (tablaCarpetasList[j].NombreCarpeta == carpeta.NombreCarpeta)
                {
                    carpeta.TablaCarpetas.Add(tablaCarpetasList[j]);
                }
                else
                {
                    carpeta.TablaCarpetas.Remove(tablaCarpetasList[j]);
                }

            }

            return carpeta;
        }

        // PUT: api/Carpeta/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCarpeta(int id, Carpeta carpeta)
        {
            if (id != carpeta.Id) return BadRequest();

            var carpetaEdit = _context.carpeta.FirstOrDefault(x => x.Id == id);

            if (carpetaEdit == null) return NotFound();

            var oLstArchivos = _context.archivo.Where(x => x.CarpetaId == carpetaEdit.Id && x.NombreArchivo != "vacio");

            var tablaCarpetasList = _context.tablaCarpetas.Where(x => x.NombreCarpeta == carpetaEdit.NombreCarpeta).ToList();

            foreach (var oTablaCarpeta in tablaCarpetasList.AsEnumerable())
            {
                oTablaCarpeta.NombreCarpeta = carpeta.NombreCarpeta;
                _context.Update(oTablaCarpeta);
                _context.SaveChanges();
            }

            carpetaEdit.NombreCarpeta = carpeta.NombreCarpeta;

            _context.Update(carpetaEdit);
            _context.SaveChanges();

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CarpetaExists(id))
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
        bool flag = true;
        // POST: api/Carpeta
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Carpeta>> PostCarpeta(Carpeta carpeta)
        {
          if (_context.carpeta == null)
          {
              return Problem("Entity set 'DataContext.carpeta'  is null.");
          }
          List<Carpeta> carpetaList = await _context.carpeta.ToListAsync();
          for(int i = 0; i < carpetaList.Count; i++)
          {
                if (carpetaList[i].NombreCarpeta == carpeta.NombreCarpeta)
                {
                    flag= false;
                }
          }
            if (!flag)
            {
                return NotFound();
            }
            else
            {
                _context.carpeta.Add(carpeta);
                await _context.SaveChangesAsync();
                CreatedAtAction("GetCarpeta", new { id = carpeta.Id }, carpeta);
                return Ok("success");
            }
        }

        // DELETE: api/Carpeta/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCarpeta(int id)
        {
            if (_context.carpeta == null)
            {
                return NotFound();
            }
            var carpeta = await _context.carpeta.FindAsync(id);
            if (carpeta == null)
            {
                return NotFound();
            }
            List<Archivo> archivosList = await _context.archivo.ToListAsync();
            for (int j = 0; j > archivosList.Count; j++)
            {
                if (carpeta.Archivos == null)
                {

                    carpeta.Archivos = new List<Archivo>();
                }
                if (archivosList[j].CarpetaId == carpeta.Id)
                {
                    carpeta.Archivos.Add(archivosList[j]);
                }
            }
            for (int j = 0; j < archivosList.Count; j++)
            {
                if (carpeta.Archivos == null)
                {

                    carpeta.Archivos = new List<Archivo>();
                }

                if (archivosList[j].NombreArchivo == "vacio")
                {
                    carpeta.Archivos.Remove(archivosList[j]);
                }
            }
            List<TablaCarpetas> tablaCarpetasList = await _context.tablaCarpetas.ToListAsync();
            for (int j = 0; j < tablaCarpetasList.Count; j++)
            {
                if (tablaCarpetasList[j].NombreCarpeta == carpeta.NombreCarpeta)
                {
                    if (carpeta.TablaCarpetas == null)
                    {

                        carpeta.TablaCarpetas = new List<TablaCarpetas>();
                    }

                    carpeta.TablaCarpetas.Add(tablaCarpetasList[j]);
                }
                else
                {
                    if (carpeta.TablaCarpetas != null)
                    {

                        carpeta.TablaCarpetas.Remove(tablaCarpetasList[j]);
                    }
                    
                }

            }
            List<Carpeta> carpetaList = await _context.carpeta.ToListAsync();
            if(carpeta.TablaCarpetas != null)
            {
                for (int i = 0; i < carpeta.TablaCarpetas.Count; i++)
                {
                    for (int j = 0; j < carpetaList.Count; j++)
                    {
                        if (carpeta.TablaCarpetas[i].CarpetaId == carpetaList[j].Id)
                        {
                            await this.DeleteCarpeta(carpetaList[j].Id);

                        }
                    }

                }
            }
            
            _context.carpeta.Remove(carpeta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CarpetaExists(int id)
        {
            return (_context.carpeta?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
