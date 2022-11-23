using LogicaNegocio.Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class PartidosController : Controller
    {
        public string UrlPartidos { get; set; }

        public PartidosController(IConfiguration configuration)
        {
            UrlPartidos = configuration.GetValue<string>("UrlPartidos");
        }
        // GET: PartidosController
        public ActionResult Index()
        {
            List<PartidoDTOViewModel> partidos = new List<PartidoDTOViewModel>();
            try
            {
                HttpResponseMessage respuestaPartido = ReadMessage();
                string txt = ObtenerBody(respuestaPartido);

                if (respuestaPartido.IsSuccessStatusCode)
                {
                    partidos = ObtenerDtoPartidos(txt);


                    if (partidos.Count == 0)
                    {
                        ViewBag.Message = "No se han encontrado resultados";
                    }
                    return View(partidos);
                } else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones. Error: " + respuestaPartido.ReasonPhrase + " " + txt;
                    return View(partidos);
                }
            }
            catch (Exception ex)
            {
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View(partidos);
            }
        }

        // GET: PartidosController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PartidosController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PartidosController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PartidosController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PartidosController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PartidosController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PartidosController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult BuscarPorGrupo(string nombreGrupo)
        {
            List<PartidoDTOViewModel> partidosPorGrupo = new List<PartidoDTOViewModel>();
            try
            {
                HttpResponseMessage respuesta = ReadMessage("/grupo/" + nombreGrupo);
                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    partidosPorGrupo = ObtenerDtoPartidos(txt);

                    if (partidosPorGrupo.Count == 0)
                    {
                        ViewBag.Message = "No se han encontrado resultados para la búsqueda";
                    }
                    return View(partidosPorGrupo);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones por grupo. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(partidosPorGrupo);
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View(partidosPorGrupo);
            }
        }

        public ActionResult BuscarPorSeleccionOPais(string nombre)
        {
            List<PartidoDTOViewModel> partidosPorSeleccionOPais = new List<PartidoDTOViewModel>();
            try
            {
                HttpResponseMessage respuesta = ReadMessage("/seleccion-pais/" + nombre);
                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    partidosPorSeleccionOPais = ObtenerDtoPartidos(txt);

                    if (partidosPorSeleccionOPais.Count == 0)
                    {
                        ViewBag.Message = "No se han encontrado resultados para la búsqueda";
                    }
                    return View(partidosPorSeleccionOPais);

                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones por grupo. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(partidosPorSeleccionOPais);
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View(partidosPorSeleccionOPais);
            }
        }

        public ActionResult BuscarPorCodigoIsoPais(string codigo)
        {
            List<PartidoDTOViewModel> partidosPorPais = new List<PartidoDTOViewModel>();
            try
            {
                HttpResponseMessage respuesta = ReadMessage("/pais/" + codigo);
                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    partidosPorPais = ObtenerDtoPartidos(txt);

                    if (partidosPorPais.Count == 0)
                    {
                        ViewBag.Message = "No se han encontrado resultados para la búsqueda";
                    }
                    return View(partidosPorPais);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones por grupo. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(partidosPorPais);
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View(partidosPorPais);
            }
        }

        public ActionResult BuscarPorFechas(string fechaDeste, string fechaHasta)
        {
            List<PartidoDTOViewModel> partidosPorFechas = new List<PartidoDTOViewModel>();
            try
            {
                HttpResponseMessage respuesta = ReadMessage("/desde/" + fechaDeste + "/hasta/" + fechaHasta);
                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    partidosPorFechas = ObtenerDtoPartidos(txt);

                    if (partidosPorFechas.Count == 0)
                    {
                        ViewBag.Message = "No se han encontrado resultados para la búsqueda";
                    }
                    return View(partidosPorFechas);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones por grupo. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(partidosPorFechas);
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View(partidosPorFechas);
            }
        }

        private List<PartidoDTOViewModel> ObtenerDtoPartidos(string txt)
        {
            List<PartidoDTOViewModel> dtoPartidos = new List<PartidoDTOViewModel>();
            List<Partido> partidos = JsonConvert.DeserializeObject<List<Partido>>(txt);
            foreach (var p in partidos)
            {
                PartidoDTOViewModel dtoPartido = new PartidoDTOViewModel();
                dtoPartido.NombreSeleccion1 = p.Seleccion1.Nombre;
                dtoPartido.NombreSeleccion2 = p.Seleccion2.Nombre;
                dtoPartido.Fecha = p.Fecha;
                dtoPartido.Hora = p.Hora.Hora;
                dtoPartido.GolesSeleccion1 = p.GolesS1;
                dtoPartido.GolesSeleccion2 = p.GolesS2;
                dtoPartido.Estado = p.Estado;
                dtoPartidos.Add(dtoPartido);
            }

            return dtoPartidos;
        }

        private string ObtenerBody(HttpResponseMessage respuesta)
        {
            HttpContent contenido = respuesta.Content;

            Task<string> tarea2 = contenido.ReadAsStringAsync();
            tarea2.Wait();

            return tarea2.Result;
        }

        private HttpResponseMessage ReadMessage(string url = "")
        {
            HttpClient cliente = new HttpClient();

            Task<HttpResponseMessage> tarea1 = cliente.GetAsync(UrlPartidos + url);
            tarea1.Wait();

            HttpResponseMessage respuesta = tarea1.Result;

            return respuesta;
        }
    }
}
