using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using GunSmithV2.Data;
using GunSmithV2.Models;

using Microsoft.AspNetCore.Cors;

namespace GunSmithV2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GunItemsController : ControllerBase
    {
        private readonly GunSmithDbContext _context;

        public GunItemsController(GunSmithDbContext context)
        {
            _context = context;
        }

        // GET: api/GunItems
        [EnableCors("Blackrock")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<GunItem>>> GetGunItems()
        {
            return await _context.GunItems.ToListAsync();
        }

        // GET: api/GunItems/5
        [HttpGet("{id}")]
        public async Task<ActionResult<GunItem>> GetGunItem(int id)
        {
            var gunItem = await _context.GunItems.FindAsync(id);

            if (gunItem == null)
            {
                return NotFound();
            }

            return gunItem;
        }

        // PUT: api/GunItems/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutGunItem(int id, GunItem gunItem)
        {
            if (id != gunItem.Id)
            {
                return BadRequest();
            }

            _context.Entry(gunItem).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!GunItemExists(id))
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

        // POST: api/GunItems
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<GunItem>> PostGunItem(GunItem gunItem)
        {
            _context.GunItems.Add(gunItem);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetGunItem", new { id = gunItem.Id }, gunItem);
        }

        // DELETE: api/GunItems/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteGunItem(int id)
        {
            var gunItem = await _context.GunItems.FindAsync(id);
            if (gunItem == null)
            {
                return NotFound();
            }

            _context.GunItems.Remove(gunItem);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool GunItemExists(int id)
        {
            return _context.GunItems.Any(e => e.Id == id);
        }
    }
}
