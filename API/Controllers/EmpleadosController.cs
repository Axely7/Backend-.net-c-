using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/empleados")]
    public class EmpleadosController : ControllerBase
    {
        private readonly StoreContext context;

        public EmpleadosController(StoreContext context)
        {
            this.context = context;

        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>> GetElements()
        {
            return await context.Empleados.ToListAsync();

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Empleado>> GetProduct(int id)
        {
            return await context.Empleados.FindAsync(id);
        }

        [HttpPost]
        public async Task<ActionResult<List<Empleado>>> AddProduct(Empleado empleado)
        {
            context.Add(empleado);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var existe = await context.Empleados.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound();

            context.Remove(new Empleado() { Id = id });
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Put(Empleado empleado, int id)
        {
            if (empleado.Id != id)
                return BadRequest("El id del auto no coincide con el id de la URL");

            var existe = await context.Empleados.AnyAsync(x => x.Id == id);
            if (!existe)
                return NotFound();

            context.Update(empleado);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}