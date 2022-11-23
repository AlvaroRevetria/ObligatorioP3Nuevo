using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebMVC.Models;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;


namespace WebMVC.Controllers
{
    public class UsuariosController : Controller
    {
        public string UrlRoles { get; set; }
        public string UrlUsuarios { get; set; }
        public UsuariosController(IConfiguration configuration)
        {
            UrlRoles = configuration.GetValue<string>("UrlRoles");
            UrlUsuarios = configuration.GetValue<string>("UrlUsuarios");

        }

        [HttpGet]
        public ActionResult Login()
        {
            HttpResponseMessage respuestaRoles = Message(UrlRoles);
            List<RolDTOViewModel> roles = new List<RolDTOViewModel>();
            if (respuestaRoles.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaRoles);
                roles = JsonConvert.DeserializeObject<List<RolDTOViewModel>>(txt);
            }
            UsuarioDTOViewModel vm = new UsuarioDTOViewModel();
            vm.Roles = roles;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(UsuarioDTOViewModel vm)
        {
            HttpResponseMessage respuestaUsuario = Message(UrlUsuarios);
            List<RolDTOViewModel> rolesUsuario = new List<RolDTOViewModel>();
            List<DTOUsuarioViewModel> usuarios = new List<DTOUsuarioViewModel>();
            if (respuestaUsuario.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaUsuario);
                usuarios = JsonConvert.DeserializeObject<List<DTOUsuarioViewModel>>(txt);
            }
            bool encontrado = false;

            foreach (DTOUsuarioViewModel u in usuarios)
            {

                if (vm.usuario.Email == u.Email && vm.usuario.Password == u.Password)
                {
                    foreach (UsuarioRolDTOViewModel ur in u.UsuarioRol)
                    {

                        HttpResponseMessage respuestaRol = Message(UrlRoles + "/" + ur.rolId);
                        if (respuestaRol.IsSuccessStatusCode)
                        {
                            string txt = ObtenerBody(respuestaRol);
                            RolDTOViewModel rol = JsonConvert.DeserializeObject<RolDTOViewModel>(txt);
                            rolesUsuario.Add(rol);
                        }
                    }
                }

                foreach (RolDTOViewModel rol in rolesUsuario)
                {

                    if (rol.Id == vm.IdRolSeleccionado)
                    {
                        encontrado = true;
                        HttpContext.Session.SetString("rol", rol.Nombre);
                    }

                }


            }
            if (!encontrado)
            {
                ViewBag.Error = " Usuario, Password o Rol incorrecto";
                HttpResponseMessage respuestaRoles = Message(UrlRoles);
                List<RolDTOViewModel> roles = new List<RolDTOViewModel>();
                if (respuestaRoles.IsSuccessStatusCode)
                {
                    string txt = ObtenerBody(respuestaRoles);
                    roles = JsonConvert.DeserializeObject<List<RolDTOViewModel>>(txt);
                }

                vm.Roles = roles;
                return View(vm);
            }
           if(HttpContext.Session.GetString("rol") == "Apostador") { return RedirectToAction(nameof(Index), "Partidos"); }

            return RedirectToAction(nameof(Index), "SeleccionesApi");
        }

        // GET: AutoresWebapiController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: AutoresWebapiController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioDTOViewModel vm)
        {
            try

            {                
                HttpClient cliente = new HttpClient();
               
                Task<HttpResponseMessage> tarea1 = cliente.PostAsJsonAsync(UrlUsuarios, vm.usuario);
                tarea1.Wait();

                HttpResponseMessage respuesta = tarea1.Result;

                if (respuesta.IsSuccessStatusCode) //status code de la serie 200
                {
                    return RedirectToAction(nameof(Login));
                }
                else
                {
                    ViewBag.Error = "No se pudo dar de alte el usuario. Error: " + ObtenerBody(respuesta);

                    return View();
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = "Ocurrió un error: " + e.Message;
                //loguear la excepción? inner exception?
                return View(vm);
            }
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            HttpContext.Session.Remove("rol");
            return RedirectToAction("Login", "Usuarios");
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

        public ActionResult ErrorRol()
        {
            
            return View();
        }
    }
}