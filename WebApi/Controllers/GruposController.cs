using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excepciones;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GruposController : ControllerBase
    {
        public IRepositorioGrupos RepoGrupos { get; set; }

        public GruposController(IRepositorioGrupos repoGrupos)
        {
            RepoGrupos = repoGrupos;
        }

        // GET: api/<GruposController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Grupo> grupos = RepoGrupos.FindAll();
                if (grupos.Count() == 0) return NotFound();
                return Ok(grupos);
            }
            catch
            {
                return StatusCode(500);
            }
        }

        // GET api/<GruposController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0) return BadRequest();
                Grupo buscado = RepoGrupos.FindById(id);
                if (buscado == null) return NotFound();
                return Ok(buscado);
            }
            catch 
            {
                return StatusCode(500);
            }
            
        }

        // POST api/<GruposController>
        [HttpPost]
        public IActionResult Post([FromBody] Grupo grupo)
        {
            try
            {
                if (grupo == null) return BadRequest("Empty Body");
                RepoGrupos.Add(grupo);
                return Created("api/grupos/" + grupo.Id, grupo);
            }
            catch (GrupoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // PUT api/<GruposController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Grupo grupo)
        {
            try
            {
                if (id == 0 || grupo == null) return BadRequest("El id no puede ser 0 o faltan datos");
                grupo.Id = id;
                RepoGrupos.Update(grupo);
                return Ok(grupo);
            }
            catch (GrupoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        // DELETE api/<GruposController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0) return BadRequest();
                RepoGrupos.Remove(id);
                return NoContent();
            }
            catch (GrupoException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("nombre/{nomGrupo}")]
        public IActionResult Get(string nomGrupo)
        {
            try
            {
                if (nomGrupo == null) return BadRequest("El nombre del grupo es obligatorio");

                IEnumerable<Grupo> grupos = RepoGrupos.DatosPartidos(nomGrupo);
                if (grupos.Count() == 0) return NotFound("No se encontraron datos para el grupo con el nombre indicado");

                return Ok(grupos);
            }
            catch
            {
                return StatusCode(500);
            }
        }
    }
}
