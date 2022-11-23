
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
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
        [Autorizacion("Invitado", "Admin")]
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
                    List<VmSeleccion> selecciones = JsonConvert.DeserializeObject<List<VmSeleccion>>(txt);
                    return View(selecciones);
                }
                else
                {
                    ViewBag.Error = "No se pudo obtener la lista de selecciones. Error: " + respuesta.ReasonPhrase + " " + txt;
                    return View(new List<VmSeleccion>());
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
        [Autorizacion("Invitado", "Admin")]
        public ActionResult Details(int id)
        {
            try
            {
                VmSeleccion seleccion = BuscarPorId(id);
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
        [Autorizacion("Admin")]
        public ActionResult Create()
        {
            SeleccionViewModel vm = new SeleccionViewModel();

            List<VmGrupo> grupos = ObtenerGrupos();
            if(grupos.Count > 0)
            {
                vm.grupos = grupos;
            }

            List<VmPais> paises = ObtenerPaises();
            if(paises.Count > 0)
            {
                vm.paises = paises;
            }                    
            
            return View(vm);
        }

        // POST: SeleccionesApiController/Create
        [Autorizacion("Admin")]
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
                    List<VmGrupo> grupos = ObtenerGrupos();
                    if (grupos.Count > 0)
                    {
                        vm.grupos = grupos;
                    }

                    List<VmPais> paises = ObtenerPaises();
                    if (paises.Count > 0)
                    {
                        vm.paises = paises;
                    }

                    ViewBag.Error = "No se pudo dar de alta la seleccion. Error: " + ObtenerBody(respuesta);
                    return View(vm);
                }
            }
            catch (Exception e)
            {
                List<VmGrupo> grupos = ObtenerGrupos();
                if (grupos.Count > 0)
                {
                    vm.grupos = grupos;
                }

                List<VmPais> paises = ObtenerPaises();
                if (paises.Count > 0)
                {
                    vm.paises = paises;
                }

                ViewBag.Error = "Ocurrió un error: " + e.Message;
                return View(vm);
            }
        }

        // GET: SeleccionesApiController/Edit/5
        [Autorizacion("Admin")]
        public ActionResult Edit(int id)
        {
            SeleccionViewModel vm = new SeleccionViewModel();
            List<VmGrupo> grupos = ObtenerGrupos();
            if (grupos.Count > 0)
            {
                vm.grupos = grupos;
            }

            List<VmPais> paises = ObtenerPaises();
            if (paises.Count > 0)
            {
                vm.paises = paises;
            }

            VmSeleccion s = BuscarPorId(id);
            vm.seleccion = s;

            return View(vm);
        }

        // POST: SeleccionesApiController/Edit/5
        [Autorizacion("Admin")]
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
                    List<VmGrupo> grupos = ObtenerGrupos();
                    if (grupos.Count > 0)
                    {
                        vm.grupos = grupos;
                    }

                    List<VmPais> paises = ObtenerPaises();
                    if (paises.Count > 0)
                    {
                        vm.paises = paises;
                    }


                    ViewBag.Error = "No se pudo actualizar la seleccion. Error: " + ObtenerBody(respuesta);
                    return View(vm);
                }
            }
            catch (Exception e)
            {
                List<VmGrupo> grupos = ObtenerGrupos();
                if (grupos.Count > 0)
                {
                    vm.grupos = grupos;
                }

                List<VmPais> paises = ObtenerPaises();
                if (paises.Count > 0)
                {
                    vm.paises = paises;
                }

                ViewBag.Error = "Ocurrió un error: " + e.Message;
                return View(vm);
            }
        }

        // GET: SeleccionesApiController/Delete/5
        [Autorizacion("Admin")]
        public ActionResult Delete(int id)
        {
            try
            {
                VmSeleccion s = BuscarPorId(id);
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
        [Autorizacion("Admin")]
        public ActionResult Delete(int id, VmSeleccion s)
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
        [Autorizacion("Invitado", "Admin")]
        private VmSeleccion BuscarPorId(int id)
            {
                VmSeleccion s = null;

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

                    s = JsonConvert.DeserializeObject<VmSeleccion>(json);
                }

                return s;
            }

        [Autorizacion("Invitado", "Admin","Apostador")]
        public ActionResult BuscarPorGrupo(string nombreGrupo)
        {
            List<SeleccionDTOViewModel> seleccionesPorGrupo = new List<SeleccionDTOViewModel>();
            try
            {
                HttpClient cliente = new HttpClient();

                List<VmGrupo> grupos = ObtenerGrupos();
                VmGrupo grupoIngresado = grupos.Where(g => g.Nombre == nombreGrupo).FirstOrDefault();

                if (grupoIngresado == null)
                {
                    ViewBag.Error = "El grupo " + nombreGrupo + " no existe";
                    return View(seleccionesPorGrupo);
                }

                Task<HttpResponseMessage> tarea1 = cliente.GetAsync(UrlSelecciones + "/grupo/" + nombreGrupo);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                string txt = ObtenerBody(respuesta);

                if (respuesta.IsSuccessStatusCode)
                {
                    seleccionesPorGrupo = JsonConvert.DeserializeObject<List<SeleccionDTOViewModel>>(txt);
                    if (seleccionesPorGrupo.Count == 0)
                    {
                        ViewBag.Message = "No se han encontrado resultados";
                    }
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

        private List<VmGrupo> ObtenerGrupos()
        {
            HttpResponseMessage respuestaGrupo = Message(UrlGrupos);
            List<VmGrupo> grupos = new List<VmGrupo>();
            if (respuestaGrupo.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaGrupo);
                grupos = JsonConvert.DeserializeObject<List<VmGrupo>>(txt);
            }

            return grupos;
        }

        private List<VmPais> ObtenerPaises()
        {
            HttpResponseMessage respuestaPais = Message(UrlPaises);
            List<VmPais> paises = new List<VmPais>();
            if (respuestaPais.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaPais);
                paises = JsonConvert.DeserializeObject<List<VmPais>>(txt);
            }
            return paises;
        }
    }
}
