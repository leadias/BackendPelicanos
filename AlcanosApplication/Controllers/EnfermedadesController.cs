using AlcanosApplication.Models;
using Microsoft.AspNetCore.Mvc;
using System.Data;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace AlcanosApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EnfermedadesController : ControllerBase
    {
        public readonly DBAPIContext _dbcontext;
        public EnfermedadesController (DBAPIContext _context) { 
           _dbcontext = _context;
        }

        // GET: api/<EnfermedadesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                DataTable lists = new DataTable();

                lists = _dbcontext.getList();

                var listItem = (from row in lists.AsEnumerable()
                                select new Enfermedad()
                                {
                                    nombre_enfermedad = row["nombre_enfermedad"].ToString()

                                }).ToList();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = listItem });
            }
            catch(Exception ex) 
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message, response = "Error obteniendo enfermedades" });
            }
           
        }


        // PUT api/<EnfermedadesController>/5
        [HttpPut("{id}")]
        public ActionResult Put(int id, [FromBody] Enfermedad value)
        {
            Enfermedad enfermedad = _dbcontext.enfermedad.Find(id);

            if(enfermedad == null)
            {
                return BadRequest("enfermedad a editar no encontrada");

            }


            try
            {

                enfermedad.nombre_enfermedad = value.nombre_enfermedad is null ? 
                    enfermedad.nombre_enfermedad : value.nombre_enfermedad;

                _dbcontext.enfermedad.Update(enfermedad);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = "Enfermedad actualizada" });

            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });
            }

        }

        [HttpPost]
        public IActionResult Store( [FromBody ]Enfermedad enfermedad)
        {

            try
            {

                _dbcontext.enfermedad.Add(enfermedad);
                _dbcontext.SaveChanges();

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = "Enfermedad Creada" });

            } catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status400BadRequest, new { mensaje = ex.Message });

            }
        }
    }
}
