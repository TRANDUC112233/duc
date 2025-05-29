using HydroponicAppServer;
using HydroponicAppServer.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using HydroponicAppServer.Models;

namespace YourNamespace.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SensorDataController : ControllerBase
    {
        private readonly AppDbContext _context;

        public SensorDataController(AppDbContext context)
        {
            _context = context;
        }

        // GET: api/SensorData
        [HttpGet]
        public async Task<ActionResult<IEnumerable<SensorData>>> GetSensorData()
        {
            return await _context.SensorData.ToListAsync();
        }

        // GET: api/SensorData/{userId}
        [HttpGet("{userId}")]
        public async Task<ActionResult<SensorData>> GetSensorData(string userId)
        {
            var sensorData = await _context.SensorData.FindAsync(userId);

            if (sensorData == null)
            {
                return NotFound();
            }

            return sensorData;
        }

        // POST: api/SensorData
        [HttpPost]
        public async Task<ActionResult<SensorData>> PostSensorData(SensorData sensorData)
        {
            _context.SensorData.Add(sensorData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSensorData), new { userId = sensorData.UserId }, sensorData);
        }

        // PUT: api/SensorData/{userId}
        [HttpPut("{userId}")]
        public async Task<IActionResult> PutSensorData(string userId, SensorData sensorData)
        {
            if (userId != sensorData.UserId)
            {
                return BadRequest();
            }

            _context.Entry(sensorData).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SensorDataExists(userId))
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

        // DELETE: api/SensorData/{userId}
        [HttpDelete("{userId}")]
        public async Task<IActionResult> DeleteSensorData(string userId)
        {
            var sensorData = await _context.SensorData.FindAsync(userId);
            if (sensorData == null)
            {
                return NotFound();
            }

            _context.SensorData.Remove(sensorData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorDataExists(string userId)
        {
            return _context.SensorData.Any(e => e.UserId == userId);
        }
    }
}