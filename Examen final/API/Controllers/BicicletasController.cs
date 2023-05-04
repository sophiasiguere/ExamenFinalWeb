using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using API.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class BicicletasController : ControllerBase
    {
        private readonly ExamenfinalContext _context;

        public BicicletasController(ExamenfinalContext context)
        {
            _context = context;
        }

        // GET: api/Bicicletas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bicicleta>>> GetBicicletas()
        {
          if (_context.Bicicletas == null)
          {
              return NotFound();
          }
            return await _context.Bicicletas.ToListAsync();
        }

        // GET: api/Bicicletas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Bicicleta>> GetBicicleta(int id)
        {
          if (_context.Bicicletas == null)
          {
              return NotFound();
          }
            var bicicleta = await _context.Bicicletas.FindAsync(id);

            if (bicicleta == null)
            {
                return NotFound();
            }

            return bicicleta;
        }

        // PUT: api/Bicicletas/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBicicleta(int id, Bicicleta bicicleta)
        {
            if (id != bicicleta.Id)
            {
                return BadRequest();
            }

            _context.Entry(bicicleta).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BicicletaExists(id))
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

        // POST: api/Bicicletas
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Bicicleta>> PostBicicleta(Bicicleta bicicleta)
        {
          if (_context.Bicicletas == null)
          {
              return Problem("Entity set 'ExamenfinalContext.Bicicletas'  is null.");
          }
            _context.Bicicletas.Add(bicicleta);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBicicleta", new { id = bicicleta.Id }, bicicleta);
        }

        // DELETE: api/Bicicletas/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBicicleta(int id)
        {
            if (_context.Bicicletas == null)
            {
                return NotFound();
            }
            var bicicleta = await _context.Bicicletas.FindAsync(id);
            if (bicicleta == null)
            {
                return NotFound();
            }

            _context.Bicicletas.Remove(bicicleta);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool BicicletaExists(int id)
        {
            return (_context.Bicicletas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
