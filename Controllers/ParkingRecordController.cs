using EstacionamientoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NuGet.Versioning;

namespace EstacionamientoAPI.Controllers
{
    [ApiController]
    [Route("api/v1/registro-parqueo")]
    public class ParkingRecordController : Controller
    {
        private readonly DbparkingContext _dbContext;

        public ParkingRecordController(DbparkingContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ParkingRecord>>> GetRegistroParqueo()
        {
            if(_dbContext.ParkingRecords == null)
            {
                return NotFound();  
            }

            return await _dbContext.ParkingRecords.ToListAsync();
        }
        // Para registrar entrada del vehiculo al parqueo
        // Solo se introduce el numero de placa, si es oficial o residente se agrega a la tabla ParkingRecord
        // En caso de no ser ninguna de las anteriores primero se registra en VehicleRegister como no residente
        [HttpPost]
        public async Task<ActionResult<ParkingRecord>> PostRegistroParqueo(string placa)
        {
            if(!ExisteVehiculo(placa.ToUpper()))
            {
                var tipoVehiculo = (from v in _dbContext.VehicleTypes where v.NameType.ToLower() == "no residente" select v).First().IdVehicleType;

                var registroVehiculo = new VehicleRegister
                {
                    PlateNumber = placa.ToUpper(),
                    IdVehicleType = tipoVehiculo,
                    CreationDate = DateTime.Now
                };
                _dbContext.VehicleRegisters.Add(registroVehiculo);
                await _dbContext.SaveChangesAsync();

                var parking = new ParkingRecord
                {
                    PlateNumber = placa.ToUpper(),
                    IsActive = true,
                    ArrivalTime = TimeOnly.FromDateTime(DateTime.Now),
                    CreationDate = DateTime.Now
                };
                _dbContext.ParkingRecords.Add(parking);
                await _dbContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetRegistroParqueo), new { id = parking.IdParkingRecord }, parking);


            }
           
            var parkingRecord = new ParkingRecord
            {
                PlateNumber = placa.ToUpper(),
                IsActive = true,
                ArrivalTime = TimeOnly.FromDateTime(DateTime.Now),
                CreationDate = DateTime.Now
            };
            _dbContext.ParkingRecords.Add(parkingRecord);
            await _dbContext.SaveChangesAsync();


            return CreatedAtAction(nameof(GetRegistroParqueo), new { id = parkingRecord.IdParkingRecord }, parkingRecord);
        }

        // Para registrar la salida del parqueo, solo se ingresa el numero de placa
        [HttpPut]
       public async Task<ActionResult> PutRegistroParqueo(string placa, ParkingRecord registro)
        {
            if(placa.ToUpper() != registro.PlateNumber.ToUpper())
            {
                return BadRequest();
            }
            if (!ExisteVehiculo(placa.ToLower()))
            {
                return NotFound($"No ha ingresado vehiculo con numero de placa: {placa}");
            }
            int id = (from v in _dbContext.VehicleRegisters where v.PlateNumber == placa.ToUpper() select v).First().IdVehicleType;
            decimal precio = Convert.ToDecimal((from v in _dbContext.VehicleTypes where v.IdVehicleType == id select v).First().RatePerMinute);
            var horaSalida = TimeOnly.FromDateTime(DateTime.Now);
            var horasParqueado = (horaSalida - registro.ArrivalTime).ToString();
            var horas = horasParqueado.Split(":");
            int minutos = (Convert.ToInt32(horas[0]) * 60) + Convert.ToInt32(horas[1]);

            var parkingRecord = new ParkingRecord
            {
                IdParkingRecord = registro.IdParkingRecord,
                PlateNumber = placa.ToUpper(),
                IsActive = registro.IsActive,
                ArrivalTime = registro.ArrivalTime,
                DepartureTime = horaSalida,
                ParkedTime = minutos,
                Payment = precio * (decimal)minutos,
                CreationDate = registro.CreationDate,
                UpdateDate = DateTime.Now
            };

            _dbContext.Entry(parkingRecord).State = EntityState.Modified;

            try
            {
                await _dbContext.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ExisteVehiculo(placa.ToUpper()))
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
    }
}
