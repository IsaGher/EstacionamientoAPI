using EstacionamientoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoAPI.Controllers
{
    [ApiController]
    [Route("api/tipoVehiculos")]
    public class VehicleTypeController : Controller
    {
        private readonly DbparkingContext _dbContext;

        public VehicleTypeController(DbparkingContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleType>>> GetTipos()
        {
            if(_dbContext.VehicleTypes == null)
            {
                return NotFound();
            }

            var datos = await _dbContext.VehicleTypes.ToListAsync();
            return await _dbContext.VehicleTypes.ToListAsync();
        }

        [HttpPost]

        public async Task<ActionResult<VehicleType>> PostTipo(VehicleType tipo)
        {
            _dbContext.VehicleTypes.Add(tipo);
            await _dbContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetTipos), new { id = tipo.IdVehicleType }, tipo);
        }

    }
}
