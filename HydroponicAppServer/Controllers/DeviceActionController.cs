using HydroponicAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace HydroponicAppServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceActionController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DeviceActionController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/DeviceAction
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DeviceAction>>> GetDeviceActions()
        {
            return await _context.DeviceAction.ToListAsync();
        }

        // GET: api/DeviceAction/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<DeviceAction>> GetDeviceAction(int id)
        {
            var deviceAction = await _context.DeviceAction.FindAsync(id);

            if (deviceAction == null)
            {
                return NotFound();
            }

            return deviceAction;
        }

        // GET: api/DeviceAction/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<DeviceAction>>> GetDeviceActionsByUser(string userId)
        {
            return await _context.DeviceAction
                .Where(da => da.UserId == userId)
                .ToListAsync();
        }

        // POST: api/DeviceAction
        [HttpPost]
        public async Task<ActionResult<DeviceAction>> PostDeviceAction(DeviceAction deviceAction)
        {
            _context.DeviceAction.Add(deviceAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeviceAction), new { id = deviceAction.Id }, deviceAction);
        }

        // PUT: api/DeviceAction/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDeviceAction(int id, DeviceAction deviceAction)
        {
            if (id != deviceAction.Id)
            {
                return BadRequest();
            }

            _context.Entry(deviceAction).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DeviceActionExists(id))
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

        // DELETE: api/DeviceAction/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDeviceAction(int id)
        {
            var deviceAction = await _context.DeviceAction.FindAsync(id);
            if (deviceAction == null)
            {
                return NotFound();
            }

            _context.DeviceAction.Remove(deviceAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceActionExists(int id)
        {
            return _context.DeviceAction.Any(e => e.Id == id);
        }
    }
}