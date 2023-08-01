using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using BlazorCrud.Server.Models;
using BlazorCrud.Shared;
using Microsoft.EntityFrameworkCore;

namespace BlazorCrud.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {

        private readonly DbcrudBlazorContext _dbContext;

        public EmpleadoController(DbcrudBlazorContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("Lista")]
        public async Task<IActionResult> Lista()
        {
            var responseApi = new ResponseAPI<List<EmpleadoDTO>>();
            var listaEmpleadoDTO = new List<EmpleadoDTO>();

            try
            {
                foreach (var item in await _dbContext.Empleados.Include(d => d.IdDepartamentoNavigation).ToListAsync())
                {
                    listaEmpleadoDTO.Add(new EmpleadoDTO
                    {
                        IdEmpleado = item.IdEmpleado,
                        Nombre = item.Nombre,
                        Apellido1 = item.Apellido1,
                        Apellido2 = item.Apellido2,
                        Telefono = item.Telefono,
                        Email = item.Email,
                        Sueldo = item.Sueldo,
                        FechaContrato = item.FechaContrato,
                        IdDepartamento = item.IdDepartamento,
                        Departamento = new DepartamentoDTO
                        {
                            IdDepartamento = item.IdDepartamentoNavigation.IdDepartamento,
                            Nombre = item.IdDepartamentoNavigation.Nombre
                        }

                    });
                }

                responseApi.EsCorrecto = true;
                responseApi.Valor = listaEmpleadoDTO;
            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpGet]
        [Route("Buscar/{id}")]
        public async Task<IActionResult> Buscar(int id)
        {
            var responseApi = new ResponseAPI<EmpleadoDTO>();
            var EmpleadoDTO = new EmpleadoDTO();

            try
            {
                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(x => x.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    EmpleadoDTO.IdEmpleado = EmpleadoDTO.IdEmpleado;
                    EmpleadoDTO.Nombre = EmpleadoDTO.Nombre;
                    EmpleadoDTO.Apellido1 = EmpleadoDTO.Apellido1;
                    EmpleadoDTO.Apellido2 = EmpleadoDTO.Apellido2;
                    EmpleadoDTO.Telefono = EmpleadoDTO.Telefono;
                    EmpleadoDTO.Email = EmpleadoDTO.Email;
                    EmpleadoDTO.Sueldo = EmpleadoDTO.Sueldo;
                    EmpleadoDTO.FechaContrato = EmpleadoDTO.FechaContrato;
                    EmpleadoDTO.IdDepartamento = EmpleadoDTO.IdDepartamento;


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = EmpleadoDTO;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No encontrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }

        [HttpPost]
        [Route("Guardar")]
        public async Task<IActionResult> Guardar(EmpleadoDTO empleado)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {
                var dbEmpleado = new Empleado
                {
                    Nombre = empleado.Nombre,
                    Apellido1 = empleado.Apellido1,
                    Apellido2 = empleado.Apellido2,
                    Telefono = empleado.Telefono,
                    Email = empleado.Email,
                    Sueldo = empleado.Sueldo,
                    FechaContrato = empleado.FechaContrato,
                    IdDepartamento = empleado.IdDepartamento
                };

                _dbContext.Empleados.Add(dbEmpleado);
                await _dbContext.SaveChangesAsync();

                if (dbEmpleado.IdEmpleado != 0)
                {
                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpleado.IdEmpleado;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "No guardado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpPut]
        [Route("Editar/{id}")]
        public async Task<IActionResult> Editar(EmpleadoDTO empleado, int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {

                    dbEmpleado.Nombre = empleado.Nombre;
                    dbEmpleado.Apellido1 = empleado.Apellido1;
                    dbEmpleado.Apellido2 = empleado.Apellido2;
                    dbEmpleado.Telefono = empleado.Telefono;
                    dbEmpleado.Email = empleado.Email;
                    dbEmpleado.Sueldo = empleado.Sueldo;
                    dbEmpleado.FechaContrato = empleado.FechaContrato;
                    dbEmpleado.IdDepartamento = empleado.IdDepartamento;


                    _dbContext.Empleados.Update(dbEmpleado);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                    responseApi.Valor = dbEmpleado.IdEmpleado;


                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }


        [HttpDelete]
        [Route("Eliminar/{id}")]
        public async Task<IActionResult> Eliminar(int id)
        {
            var responseApi = new ResponseAPI<int>();

            try
            {

                var dbEmpleado = await _dbContext.Empleados.FirstOrDefaultAsync(e => e.IdEmpleado == id);

                if (dbEmpleado != null)
                {
                    _dbContext.Empleados.Remove(dbEmpleado);
                    await _dbContext.SaveChangesAsync();


                    responseApi.EsCorrecto = true;
                }
                else
                {
                    responseApi.EsCorrecto = false;
                    responseApi.Mensaje = "Empleado no econtrado";
                }

            }
            catch (Exception ex)
            {

                responseApi.EsCorrecto = false;
                responseApi.Mensaje = ex.Message;
            }

            return Ok(responseApi);
        }
    }
}
