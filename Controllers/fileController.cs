using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Operations;
using Microsoft.EntityFrameworkCore;
using System.Data.Entity;
using WeSyncBackend.Dtos;

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
        public async Task<ActionResult<IEnumerable<FisierDTO>>> GetFisiers(string? path)
        {
          if (_context.Fisiers == null)
          {
              return NotFound();
          }
            if (string.IsNullOrEmpty(path))
            {
                return _context.Fisiers.Select(x => new FisierDTO(x)).ToList();
            }
            else
            {
                return _context.Fisiers
                    .Where(el => el.VirtualPath.Trim('/').Equals(path.Trim('/')))
                    .Select(x => new FisierDTO(x))
                    .ToList();
            }
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

        [HttpGet("byUserEmail/{userEmail}")]
        public async Task<ActionResult<IEnumerable<FisierDTO>>> GetFisiersByUserEmail(string userEmail)
        {
            if (_context.Fisiers == null)
            {
                return NotFound();
            }

            return _context.Fisiers
                .Where(f => f.Owner == userEmail)
                .Select(x => new FisierDTO(x))
                .ToList();
        }

        [HttpPost("folder")]
        public async Task<ActionResult> CreateFolder(CreateFolderReq req)
        {
            Fisier fisier = new()
            {
                Name = req.folderName + ".dir",
                Size = 0,
                Owner = req.Owner,
                VirtualPath = req.Virtualpath
            };
            _context.Fisiers.Add(fisier);
            await _context.SaveChangesAsync();
            return Ok();
    }


        // POST: api/file
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Fisier>> PostFisier(IEnumerable<IFormFile> fisiers, [FromForm] string Owner, [FromForm] string VirtualPath)
        {
            if (_context.Fisiers == null)
            {
                return Problem("Entity set 'AppDbContext.Fisiers'  is null.");
            }

            
            foreach (IFormFile file in fisiers)
            {

                if (file.Name.EndsWith(".dir"))
                    continue;

                Fisier fisier = new()
                {
                    Name = file.FileName,
                    Size = file.Length,
                    Owner = Owner,
                    VirtualPath = VirtualPath
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

    }
}
