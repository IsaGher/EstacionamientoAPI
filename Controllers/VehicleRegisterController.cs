using EstacionamientoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace EstacionamientoAPI.Controllers
{
    [ApiController]
    [Route("api/v1/registro-vehiculos")]
    public class VehicleRegisterController : Controller
    {
        private readonly DbparkingContext _dbContext;
        
        public VehicleRegisterController(DbparkingContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Obtener todos los vehiculos registrados
        [HttpGet]
        public async Task<ActionResult<IEnumerable<VehicleRegister>>> GetRegistroVehiculo()
        {
            if(_dbContext.VehicleRegisters == null)
            {
                return NotFound();
            }

            //var datos = await _dbContext.VehicleRegisters.ToListAsync();
            return await _dbContext.VehicleRegisters.ToListAsync();
        }

        // Buscar los vehiculos registrados por el tipo a que pertenece: oficial, residente, no residente
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<VehicleRegister>>> GetTipoVehiculoID(int id)
        {
            if(_dbContext.VehicleRegisters == null)
            {
                return NotFound();
            }
            var listaTipoVehiculos = from v in _dbContext.VehicleRegisters select v;

            if(id != null)
            {
                listaTipoVehiculos = listaTipoVehiculos.Where(t => t.IdVehicleType == id);
            }

            if(listaTipoVehiculos == null)
            {
                return NotFound();
            }
            return await listaTipoVehiculos.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<VehicleRegister>> PostRegistroVehiculo(VehicleRegister registro)
        {
            if (!ExisteTipo(registro.IdVehicleType))
            {
                return NotFound("No existe tipo de vehiculo");
            }
            registro.CreationDate = DateTime.Now;
            registro.PlateNumber = registro.PlateNumber.ToUpper();
            _dbContext.VehicleRegisters.Add(registro);
            await _dbContext.SaveChangesAsync();
 
            return CreatedAtAction(nameof(GetRegistroVehiculo), new { id = registro.PlateNumber }, registro);

        }

        [HttpPut]
        public async Task<ActionResult> PutRegistroVehiculo(string id, VehicleRegister registro)
        {
            if(id.ToUpper() != registro.PlateNumber.ToUpper())
            {
                return BadRequest();
            }

            if (!ExisteTipo(registro.IdVehicleType))
            {
                return NotFound("No existe tipo de vehiculo");
            }

            registro.UpdateDate = DateTime.Now;
            registro.PlateNumber = registro.PlateNumber.ToUpper();
            _dbContext.Entry(registro).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteVehiculo(id))
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

        private bool ExisteVehiculo(string id)
        {
            return (_dbContext.VehicleRegisters?.Any(i => i.PlateNumber == id)).GetValueOrDefault();
        }
        private bool ExisteTipo(int id)
        {
            return (_dbContext.VehicleTypes?.Any(i => i.IdVehicleType == id)).GetValueOrDefault();
        }

    }
}
