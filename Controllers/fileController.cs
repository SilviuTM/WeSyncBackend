using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WeSyncBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class fileController : ControllerBase
    {
        private readonly FisierDb _context;

        public fileController(FisierDb context)
        {
            _context = context;
        }

        // GET: api/file
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FisierDTO>>> GetFisiers()
        {
          if (_context.Fisiers == null)
          {
              return NotFound();
          }
            return await _context.Fisiers.Select(x => new FisierDTO(x)).ToListAsync();
        }

        // GET: api/file/5
        [HttpGet("{id}")]
        public async Task<ActionResult> GetFisier(int id)
        {
          if (_context.Fisiers == null)
          {
              return NotFound();
          }
            var fisier = await _context.Fisiers.FindAsync(id);

            if (fisier == null)
            {
                return NotFound();
            }

            // Return the file as a downloadable attachment
            return File(fisier.content, "application/octet-stream", fisier.name);
        }

        // PUT: api/file/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFisier(int id, Fisier fisier)
        {
            if (id != fisier.id)
            {
                return BadRequest();
            }

            _context.Entry(fisier).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FisierExists(id))
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

        // POST: api/file
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fisier>> PostFisier(IEnumerable<IFormFile> fisiers)
        {
            if (_context.Fisiers == null)
            {
                return Problem("Entity set 'FisierDb.Fisiers'  is null.");
            }
            
            foreach (IFormFile file in fisiers)
            {
                Fisier fisier = new()
                {
                    name = file.FileName,
                    size = file.Length
                };

                fisier.content = new byte[file.Length];
                MemoryStream ms = new(fisier.content);
                file.CopyTo(ms);
                
                _context.Fisiers.Add(fisier);
            }

            await _context.SaveChangesAsync();

            //  return CreatedAtAction("GetFisier", new { id = fisier.Id }, fisier);
            return Ok();
        }

        // DELETE: api/file/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFisier(int id)
        {
            if (_context.Fisiers == null)
            {
                return NotFound();
            }
            var fisier = await _context.Fisiers.FindAsync(id);
            if (fisier == null)
            {
                return NotFound();
            }

            _context.Fisiers.Remove(fisier);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool FisierExists(int id)
        {
            return (_context.Fisiers?.Any(e => e.id == id)).GetValueOrDefault();
        }
    }
}
