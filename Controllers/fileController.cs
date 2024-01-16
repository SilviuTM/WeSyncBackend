using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace WeSyncBackend.Controllers
{
    [Route("api/file")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly AppDbContext _context;

        public FileController(AppDbContext context)
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
        [HttpGet("{Id}")]
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
            return File(fisier.Content, "application/octet-stream", fisier.Name);
        }

        // PUT: api/file/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{Id}")]
        public async Task<IActionResult> PutFisier(int id, Fisier fisier)
        {
            if (id != fisier.Id)
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
        public async Task<ActionResult<Fisier>> PostFisier(IEnumerable<IFormFile> fisiers, [FromForm] string Owner)
        {
            if (_context.Fisiers == null)
            {
                return Problem("Entity set 'AppDbContext.Fisiers'  is null.");
            }
            
            foreach (IFormFile file in fisiers)
            {
                Fisier fisier = new()
                {
                    Name = file.FileName,
                    Size = file.Length,
                    Owner = Owner
                };

                fisier.Content = new byte[file.Length];
                MemoryStream ms = new(fisier.Content);
                file.CopyTo(ms);
                
                _context.Fisiers.Add(fisier);
            }

            await _context.SaveChangesAsync();

            //return CreatedAtAction("GetFisier", new { id = fisier.Id }, fisier);
            return Ok();
        }

        // DELETE: api/file/5
        [HttpDelete("{Id}")]
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

            return Ok();
        }

        private bool FisierExists(int id)
        {
            return (_context.Fisiers?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
