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
            catch (PartidoException)
            {
                throw;
            }
            catch (HoraException)
            {
                throw;
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
    }
}
