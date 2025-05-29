using HydroponicAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // GET: api/DeviceAction/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<DeviceAction>> GetDeviceAction(string userId)
        {
            var deviceAction = await _context.DeviceAction.FindAsync(userId);

            if (deviceAction == null)
            {
                return NotFound();
            }

            return deviceAction;
        }

        // POST: api/DeviceAction
        [HttpPost]
        public async Task<ActionResult<DeviceAction>> PostDeviceAction(DeviceAction deviceAction)
        {
            _context.DeviceAction.Add(deviceAction);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetDeviceAction), new { userId = deviceAction.UserId }, deviceAction);
        }

        // PUT: api/DeviceAction/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutDeviceAction(string userId, DeviceAction deviceAction)
        {
            if (userId != deviceAction.UserId)
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
                if (!DeviceActionExists(userId))
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

        // DELETE: api/DeviceAction/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteDeviceAction(string userId)
        {
            var deviceAction = await _context.DeviceAction.FindAsync(userId);
            if (deviceAction == null)
            {
                return NotFound();
            }

            _context.DeviceAction.Remove(deviceAction);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DeviceActionExists(string userId)
        {
            return _context.DeviceAction.Any(e => e.UserId == userId);
        }
    }
}