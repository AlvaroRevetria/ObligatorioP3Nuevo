﻿using LogicaNegocio.Dominio;
using LogicaNegocio.InterfacesRepositorios;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Excepciones;
using DTOS;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SeleccionesController : ControllerBase
    {
        public IRepositorioSelecciones RepoSelecciones { get; set; }

        public SeleccionesController(IRepositorioSelecciones repoSelecciones)
        {
            RepoSelecciones = repoSelecciones;
        }

         // GET: api/<SeleccionesController>
        [HttpGet]
        public IActionResult Get()
        {
            try
            {
                IEnumerable<Seleccion> selecciones = RepoSelecciones.FindAll();
                if (selecciones.Count() == 0) return NotFound();
                return Ok(selecciones);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }

        // GET api/<SeleccionesController>/5
        [HttpGet("{id}")]
        public IActionResult Get(int id)
        {
            try
            {
                if (id == 0) return BadRequest();
                Seleccion buscado = RepoSelecciones.FindById(id);
                if (buscado == null) return NotFound();
                return Ok(buscado);
            }
            catch
            {
                return StatusCode(500);
            }

        }

        // GET api/selecciones/grupo/A
        [HttpGet("grupo/{nombre}")]
        public IActionResult GetTablaPos(string nombre)
        {
            try
            {

                IEnumerable<Seleccion> seleccionesGrupo = RepoSelecciones.SeleccionesPorGrupo(nombre);
                if (seleccionesGrupo.Count() == 0) return NotFound("No existen resultados para el grupo: " + nombre);

                List<DTOSeleccion> seleccionesResult = new List<DTOSeleccion>();

                foreach (var s in seleccionesGrupo)
                {
                    DTOSeleccion dtoSeleccion = new DTOSeleccion();
                    int golesAFavor = RepoSelecciones.CalcularGolesAFavor(s);
                    int golesEnContra = RepoSelecciones.CalcularGolesEnContra(s);
                    int diferenciaDeGoles = golesAFavor - golesEnContra;
                    dtoSeleccion.Nombre = s.Nombre;
                    dtoSeleccion.Puntos = s.Puntos;
                    dtoSeleccion.GolesAFavor = golesAFavor;
                    dtoSeleccion.GolesEnContra = golesEnContra;
                    dtoSeleccion.DifGoles = diferenciaDeGoles;
                    dtoSeleccion.Bandera = s.Pais.Bandera;

                    seleccionesResult.Add(dtoSeleccion);
                }
                if (seleccionesResult.Count() == 0) return NotFound();
               
                return Ok(seleccionesResult.OrderByDescending(s => s.Puntos));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex);
            }
        }
        // POST api/<SeleccionesController>
        [HttpPost]
        public IActionResult Post([FromBody] Seleccion seleccion)
        {
            try
            {
                if (seleccion == null) return BadRequest("Body vacío");
                RepoSelecciones.Add(seleccion);
                return Created("api/selecciones/" + seleccion.Id, seleccion);
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

        // PUT api/<SeleccionesController>/5
        [HttpPut("{id}")]
        public IActionResult Put(int id, [FromBody] Seleccion seleccion)
        {
           try
            {
                if (id == 0 || seleccion == null) return BadRequest("El id no puede ser 0 o faltan datos");
                seleccion.Id = id;
                RepoSelecciones.Update(seleccion);
                return Ok(seleccion);
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

        // DELETE api/<SeleccionesController>/5
        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                if (id == 0) return BadRequest();
                RepoSelecciones.Remove(id);
                return NoContent();
            }
            catch (SeleccionException ex)
            {
                return BadRequest(ex);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
