using EstacionamientoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Text;

namespace EstacionamientoAPI.Controllers
{
    [ApiController]
    [Route("api/v1/tipo-vehiculos")]
    public class VehicleTypeController : Controller
    {
        private readonly DbparkingContext _dbContext;

        public VehicleTypeController(DbparkingContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetTipoVehiculo()
        {
            if(_dbContext.VehicleTypes == null)
            {
                return NotFound();
            }

            return await _dbContext.VehicleTypes.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<VehicleType>> PostTipoVehiculo(VehicleType tipo)
        {
            tipo.CreationDate = DateTime.Now;
            _dbContext.VehicleTypes.Add(tipo);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipoVehiculo), new { id = tipo.IdVehicleType }, tipo);
        }

        [HttpPut]
        public async Task<IActionResult> PutTipoVehiculo(int id, VehicleType tipo)
        {
            if(id != tipo.IdVehicleType)
            {
                return BadRequest();
            }
            tipo.UpdateDate = DateTime.Now;
            _dbContext.Entry(tipo).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteTipoVehiculo(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();

        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTipoVehiculo(int id)
        {
            if(_dbContext.VehicleTypes == null)
            {
                return NotFound();
            }
            var tipo = await _dbContext.VehicleTypes.FindAsync(id);
            if(tipo == null)
            {
                return NotFound();
            }

            _dbContext.VehicleTypes.Remove(tipo);
            await _dbContext.SaveChangesAsync();

            return Ok();
        }

        private bool ExisteTipoVehiculo(int id)
        {
            return(_dbContext.VehicleTypes?.Any(i => i.IdVehicleType == id)).GetValueOrDefault();
        }

    }
}
