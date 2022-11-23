using DTOS;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoGrupos;
using LogicaAplicacion.InterfacesCasosUso.ICasosUsoPais;
using LogicaNegocio.Dominio;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Filtros;
using WebMVC.Models;

namespace WebMVC.Controllers
{
    public class SeleccionesApiController : Controller
       
    {
        public string UrlSelecciones { get; set; }
        public string UrlGrupos { get; set; }
        public string UrlPaises{ get; set; }
        public SeleccionesApiController(IConfiguration configuration)
        {
            UrlSelecciones = configuration.GetValue<string>("UrlSelecciones");
            UrlGrupos= configuration.GetValue<string>("UrlGrupos");
            UrlPaises= configuration.GetValue<string>("UrlPaises");
        }

        // GET: S
        // eleccionesApiController
        public ActionResult Index()
        {
            try
            {
                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.GetAsync(UrlSelecciones);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode) //status code de la serie 200
                {
                    List<Seleccion> selecciones = JsonConvert.DeserializeObject<List<Seleccion>>(txt);
                    return View(selecciones);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(new List<Seleccion>());
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View();
            }
        }
    

        // GET: SeleccionesApiController/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                Seleccion seleccion = BuscarPorId(id);
                return View(seleccion);
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View();
            }
        }

        // GET: SeleccionesApiController/Create
        public ActionResult Create()
        {
            HttpResponseMessage respuestaGrupo= Message(UrlGrupos);
            List<Grupo> grupos = new List<Grupo>();
            if (respuestaGrupo.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaGrupo);
                grupos = JsonConvert.DeserializeObject<List<Grupo>>(txt);
            }
            HttpResponseMessage respuestaPais = Message(UrlPaises);
            List<Pais>paises = new List<Pais>();
            if (respuestaPais.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaPais);
                paises = JsonConvert.DeserializeObject<List<Pais>>(txt);
            }


            SeleccionViewModel vm = new SeleccionViewModel();
            vm.paises = paises;
            vm.grupos = grupos;
            return View(vm);
        }

        // POST: SeleccionesApiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(SeleccionViewModel vm)
        {
            try
            {
                vm.seleccion.PaisId = vm.IdPaisSeleccionado;
                vm.seleccion.GrupoId = vm.IdGrupoSeleccionado;
                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.PostAsJsonAsync(UrlSelecciones, vm.seleccion);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    HttpResponseMessage respuestaGrupo = Message(UrlGrupos);
                    List<Grupo> grupos = new List<Grupo>();
                    if (respuestaGrupo.IsSuccessStatusCode)
                    {
                        string txt = ObtenerBody(respuestaGrupo);
                        grupos = JsonConvert.DeserializeObject<List<Grupo>>(txt);
                    }
                    HttpResponseMessage respuestaPais = Message(UrlPaises);
                    List<Pais> paises = new List<Pais>();
                    if (respuestaPais.IsSuccessStatusCode)
                    {
                        string txt = ObtenerBody(respuestaPais);
                        paises = JsonConvert.DeserializeObject<List<Pais>>(txt);
                    }

                    ViewBag.Error = "No se pudo dar de alta la seleccion. Error: " + ObtenerBody(respuesta);
                    vm.paises = paises;
                    vm.grupos = grupos;
                    return View(vm);
                }
            }
            catch (Exception e)
            {
                HttpResponseMessage respuestaGrupo = Message(UrlGrupos);
                List<Grupo> grupos = new List<Grupo>();
                if (respuestaGrupo.IsSuccessStatusCode)
                {
                    string txt = ObtenerBody(respuestaGrupo);
                    grupos = JsonConvert.DeserializeObject<List<Grupo>>(txt);
                }
                HttpResponseMessage respuestaPais = Message(UrlPaises);
                List<Pais> paises = new List<Pais>();
                if (respuestaPais.IsSuccessStatusCode)
                {
                    string txt = ObtenerBody(respuestaPais);
                    paises = JsonConvert.DeserializeObject<List<Pais>>(txt);
                }

                ViewBag.Error = "Ocurrió un error: " + e.Message;
                vm.paises = paises;
                vm.grupos = grupos;
                return View(vm);
            }
        }

        // GET: SeleccionesApiController/Edit/5
        public ActionResult Edit(int id)
        {
            HttpResponseMessage respuestaGrupo = Message(UrlGrupos);
            List<Grupo> grupos = new List<Grupo>();
            if (respuestaGrupo.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaGrupo);
                grupos = JsonConvert.DeserializeObject<List<Grupo>>(txt);
            }
            HttpResponseMessage respuestaPais = Message(UrlPaises);
            List<Pais> paises = new List<Pais>();
            if (respuestaPais.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaPais);
                paises = JsonConvert.DeserializeObject<List<Pais>>(txt);
            }

           
            
            Seleccion s = BuscarPorId(id);
                SeleccionViewModel vm = new SeleccionViewModel();
                vm.seleccion = s;
                vm.paises = paises;
                vm.grupos = grupos;

            return View(vm);
        }

        // POST: SeleccionesApiController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, SeleccionViewModel vm)
        {
            try
            {
                vm.seleccion.PaisId = vm.IdPaisSeleccionado;
                vm.seleccion.GrupoId = vm.IdGrupoSeleccionado;
                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.PutAsJsonAsync(UrlSelecciones + "/" + vm.seleccion.Id, vm.seleccion);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    HttpResponseMessage respuestaGrupo = Message(UrlGrupos);
                    List<Grupo> grupos = new List<Grupo>();
                    if (respuestaGrupo.IsSuccessStatusCode)
                    {
                        string txt = ObtenerBody(respuestaGrupo);
                        grupos = JsonConvert.DeserializeObject<List<Grupo>>(txt);
                    }
                    HttpResponseMessage respuestaPais = Message(UrlPaises);
                    List<Pais> paises = new List<Pais>();
                    if (respuestaPais.IsSuccessStatusCode)
                    {
                        string txt = ObtenerBody(respuestaPais);
                        paises = JsonConvert.DeserializeObject<List<Pais>>(txt);
                    }


                    ViewBag.Error = "No se pudo actualizar la seleccion. Error: " + ObtenerBody(respuesta);
                    vm.paises = paises;
                    vm.grupos = grupos;
                    return View(vm);
                }
            }
            catch (Exception e)
            {
                HttpResponseMessage respuestaGrupo = Message(UrlGrupos);
                List<Grupo> grupos = new List<Grupo>();
                if (respuestaGrupo.IsSuccessStatusCode)
                {
                    string txt = ObtenerBody(respuestaGrupo);
                    grupos = JsonConvert.DeserializeObject<List<Grupo>>(txt);
                }
                HttpResponseMessage respuestaPais = Message(UrlPaises);
                List<Pais> paises = new List<Pais>();
                if (respuestaPais.IsSuccessStatusCode)
                {
                    string txt = ObtenerBody(respuestaPais);
                    paises = JsonConvert.DeserializeObject<List<Pais>>(txt);
                }

                ViewBag.Error = "Ocurrió un error: " + e.Message;
                vm.paises = paises;
                vm.grupos = grupos;
                return View(vm);
            }
        }

        // GET: SeleccionesApiController/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                Seleccion s = BuscarPorId(id);
                if (s == null)
                {
                    ViewBag.Error = "No existe el seleccion a borrar";
                }
                return View(s);
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View();
            }
        }

        // POST: SeleccionesApiController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Seleccion s)
        {
            try
            {
                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.DeleteAsync(UrlSelecciones + "/" + id);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                HttpContent contenido = respuesta.Content;

                if (respuesta.IsSuccessStatusCode) //status code de la serie 200
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.Error = "No se puede hacer la eliminación. Satus code: " + respuesta.ReasonPhrase +
                                    " | Descripción: " + ObtenerBody(respuesta);
                    s = BuscarPorId(id);
                    return View(s);
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                s = BuscarPorId(id);
                return View(s);
            }
        }
        private Seleccion BuscarPorId(int id)
            {
                Seleccion s = null;

                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.GetAsync(UrlSelecciones + "/" + id);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                if (respuesta.IsSuccessStatusCode)
                {
                    HttpContent contenido = respuesta.Content;

                    Task<string> tarea2 = contenido.ReadAsStringAsync();
                    tarea2.Wait();

                    string json = tarea2.Result;

                    s = JsonConvert.DeserializeObject<Seleccion>(json);
                }

                return s;
            }

        [Autorizacion("Apostador", "Admin")]
        public ActionResult BuscarPorGrupo(string nombreGrupo)
        {
            List<SeleccionDTOViewModel> seleccionesPorGrupo = new List<SeleccionDTOViewModel>();
            try
            {
                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.GetAsync(UrlSelecciones + "/grupo/" + nombreGrupo);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    seleccionesPorGrupo = JsonConvert.DeserializeObject<List<SeleccionDTOViewModel>>(txt);
                    return View(seleccionesPorGrupo);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones por grupo. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(seleccionesPorGrupo);
                }
            }
            catch (Exception ex)
            {
                //Log? 
                ViewBag.Error = "Ups! Ocurrión un error " + ex.Message;
                return View(seleccionesPorGrupo);
            }
        }
        private string ObtenerBody(HttpResponseMessage respuesta)
        {
            HttpContent contenido = respuesta.Content;

            Task<string> tarea2 = contenido.ReadAsStringAsync();
            tarea2.Wait();

            return tarea2.Result;
        }
        private HttpResponseMessage Message(string url)
            {
                HttpClient cliente = new HttpClient();

                Task<HttpResponseMessage> tarea1 = cliente.GetAsync(url);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                return respuesta;
            }

    }
}
