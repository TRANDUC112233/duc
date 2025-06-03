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

        // GET: api/SensorData/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<SensorData>> GetSensorData(int id)
        {
            var sensorData = await _context.SensorData.FindAsync(id);

            if (sensorData == null)
            {
                return NotFound();
            }

            return sensorData;
        }

        // GET: api/SensorData/by-user/{userId}
        [HttpGet("by-user/{userId}")]
        public async Task<ActionResult<IEnumerable<SensorData>>> GetSensorDataByUser(string userId)
        {
            return await _context.SensorData
                .Where(sd => sd.UserId == userId)
                .ToListAsync();
        }

        // POST: api/SensorData
        [HttpPost]
        public async Task<ActionResult<SensorData>> PostSensorData(SensorData sensorData)
        {
            _context.SensorData.Add(sensorData);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetSensorData), new { id = sensorData.Id }, sensorData);
        }

        // PUT: api/SensorData/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSensorData(int id, SensorData sensorData)
        {
            if (id != sensorData.Id)
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
                if (!SensorDataExists(id))
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

        // DELETE: api/SensorData/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSensorData(int id)
        {
            var sensorData = await _context.SensorData.FindAsync(id);
            if (sensorData == null)
            {
                return NotFound();
            }

            _context.SensorData.Remove(sensorData);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool SensorDataExists(int id)
        {
            return _context.SensorData.Any(e => e.Id == id);
        }
    }
}