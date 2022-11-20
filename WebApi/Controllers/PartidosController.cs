using Excepciones;
using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartidosController : ControllerBase
    {
        public IRepositorioPartidos RepoPartidos { get; set; }

        public PartidosController(IRepositorioPartidos repoPartidos)
        {
            RepoPartidos = repoPartidos;
        }
        // GET: api/<PartidosController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Partido> partidos = RepoPartidos.FindAll();
                if (partidos.Count() == 0) return NotFound();
                return Ok(partidos);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // GET api/<PartidosController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0) return BadRequest();
                Partido buscado = RepoPartidos.FindById(id);
                if (buscado == null) return NotFound();
                return Ok(buscado);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // POST api/<PartidosController>
        [HttpPost]
        public IActionResult Post([FromBody] Partido partido)
        {
            try
            {
                if (partido == null) return BadRequest("Body vacío");
                partido.Estado = "Pendiente";
                RepoPartidos.Add(partido);
                return Created("api/partidos/" + partido.Id, partido);
            }
            catch (PartidoException e)
            {
                return BadRequest(e.Message);
            }
            catch (HoraException e)
            {
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<PartidosController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Partido partido)
        {
            try
            {
                if (id == 0 || partido == null) return BadRequest("El id no puede ser 0 o faltan datos");
                partido.Id = id;
                RepoPartidos.Update(partido);
                return Ok(partido);
            }
            catch (SeleccionException)
            {
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<PartidosController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }

        // GET api/<PartidosController>/gupo/A
        [HttpGet("grupo/{nombreGrupo}")]
        public IActionResult GetBuscarPorGrupo(string nombreGrupo)
        {
            try
            {
                IEnumerable<Partido> partidos = RepoPartidos.BuscarPorGurpo(nombreGrupo);
                return Ok(partidos);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }
            
        }

        // GET api/<PartidosController>/seleccion/nombre
        [HttpGet("seleccion/{nombre}")]
        public IActionResult GetBuscarPorSeleccion(string nombre)
        {
            try
            {
                IEnumerable<Partido> partidos = RepoPartidos.BuscarPorSeleccionOPais(nombre);
                return Ok(partidos);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        // GET api/<PartidosController>/seleccion/nombre
        [HttpGet("pais/{codigo}")]
        public IActionResult GetBuscarPorCodigoIsoPais(string codigo)
        {
            try
            {
                IEnumerable<Partido> partidos = RepoPartidos.BuscarPorCodigoIsoPais(codigo);
                return Ok(partidos);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }

        // GET api/<PartidosController>/seleccion/nombre
        [HttpGet("desde/{fechaDeste}/hasta/{fechaHasta}")]
        public IActionResult GetBuscarPorFechas(string fechaDeste, string fechaHasta)
        {
            try
            {
                DateTime fdesde = new DateTime();
                bool ok1 = DateTime.TryParse(fechaDeste, out fdesde);

                DateTime fhasta = new DateTime();
                bool ok2 = DateTime.TryParse(fechaHasta, out fhasta);

                if (!ok1 || !ok2) return BadRequest("La fecha no es válida");
                IEnumerable<Partido> partidos = RepoPartidos.BuscarPorFechas(fdesde, fhasta);
                return Ok(partidos);
            }
            catch (Exception e)
            {

                return StatusCode(500, e.Message);
            }

        }
    }
}
