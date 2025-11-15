using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using DocumentService.Models;

namespace DocumentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        private readonly DocumentServiceContext _context;

        public DocumentsController(DocumentServiceContext context)
        {
            _context = context;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Documents>>> GetDocuments()
        {
          if (_context.Documents == null)
          {
              return NotFound();
          }
            return await _context.Documents.ToListAsync();
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Documents>> GetDocuments(Guid id)
        {
          if (_context.Documents == null)
          {
              return NotFound();
          }
            var documents = await _context.Documents.FindAsync(id);

            if (documents == null)
            {
                return NotFound();
            }

            return documents;
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDocuments(Guid id, Documents documents)
        {
            if (id != documents.DocumentId)
            {
                return BadRequest();
            }

            _context.Entry(documents).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DocumentsExists(id))
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

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Documents>> PostDocuments(Documents documents)
        {
          if (_context.Documents == null)
          {
              return Problem("Entity set 'DocumentServiceContext.Documents'  is null.");
          }
            _context.Documents.Add(documents);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetDocuments", new { id = documents.DocumentId }, documents);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocuments(Guid id)
        {
            if (_context.Documents == null)
            {
                return NotFound();
            }
            var documents = await _context.Documents.FindAsync(id);
            if (documents == null)
            {
                return NotFound();
            }

            _context.Documents.Remove(documents);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DocumentsExists(Guid id)
        {
            return (_context.Documents?.Any(e => e.DocumentId == id)).GetValueOrDefault();
        }
    }
}
