using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LicenseService.Models;
using Microsoft.AspNetCore.Authorization;

namespace LicenseService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseServicesController : ControllerBase
    {
        private readonly LicenseServiceContext _context;

        public LicenseServicesController(LicenseServiceContext context)
        {
            _context = context;
        }

        // GET: api/Licenses
        //[Authorize]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<License>>> GetLicense()
        {
          if (_context.License == null)
          {
              return NotFound();
          }
            return await _context.License.ToListAsync();
        }

        // GET: api/Licenses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<License>> GetLicense(Guid id)
        {
          if (_context.License == null)
          {
              return NotFound();
          }
            var license = await _context.License.FindAsync(id);

            if (license == null)
            {
                return NotFound();
            }

            return license;
        }

        // PUT: api/Licenses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLicense(Guid id, License license)
        {
            if (id != license.LicenseId)
            {
                return BadRequest();
            }

            _context.Entry(license).State = EntityState.Modified;

            try
            {
                _context.License.Update(license);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LicenseExists(id))
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

        // POST: api/Licenses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<License>> PostLicense(License license)
        {
          if (_context.License == null)
          {
              return Problem("Entity set 'LicenseServiceContext.License'  is null.");
          }
            await _context.License.AddAsync(license);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLicense", new { id = license.LicenseId }, license);
        }

        // DELETE: api/Licenses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLicense(Guid id)
        {
            if (_context.License == null)
            {
                return NotFound();
            }
            var license = await _context.License.FindAsync(id);
            if (license == null)
            {
                return NotFound();
            }

            _context.License.Remove(license);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LicenseExists(Guid id)
        {
            return (_context.License?.Any(e => e.LicenseId == id)).GetValueOrDefault();
        }
        
    }
}
