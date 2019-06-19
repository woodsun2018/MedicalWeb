using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MedicalWeb.Models;
using ShareLibrary;

namespace MedicalWeb.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BioValuesController : ControllerBase
    {
        private readonly MedicalWebContext _context;

        public BioValuesController(MedicalWebContext context)
        {
            _context = context;
        }

        // GET: api/BioValues
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BioValues>>> GetBioValues()
        {
            return await _context.BioValues.ToListAsync();
        }

        // GET: api/BioValues/5
        [HttpGet("{id}")]
        public async Task<ActionResult<BioValues>> GetBioValues(Guid id)
        {
            var bioValues = await _context.BioValues.FindAsync(id);

            if (bioValues == null)
            {
                return NotFound();
            }

            return bioValues;
        }

        // PUT: api/BioValues/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBioValues(Guid id, BioValues bioValues)
        {
            if (id != bioValues.ID)
            {
                return BadRequest();
            }

            _context.Entry(bioValues).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BioValuesExists(id))
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

        // POST: api/BioValues
        [HttpPost]
        public async Task<ActionResult<BioValues>> PostBioValues(BioValues bioValues)
        {
            _context.BioValues.Add(bioValues);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBioValues", new { id = bioValues.ID }, bioValues);
        }

        // DELETE: api/BioValues/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BioValues>> DeleteBioValues(Guid id)
        {
            var bioValues = await _context.BioValues.FindAsync(id);
            if (bioValues == null)
            {
                return NotFound();
            }

            _context.BioValues.Remove(bioValues);
            await _context.SaveChangesAsync();

            return bioValues;
        }

        private bool BioValuesExists(Guid id)
        {
            return _context.BioValues.Any(e => e.ID == id);
        }
    }
}
