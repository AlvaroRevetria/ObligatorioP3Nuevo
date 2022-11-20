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
using DTOS;


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
            List<DTORol> roles = new List<DTORol>();
            if (respuestaRoles.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaRoles);
                roles = JsonConvert.DeserializeObject<List<DTORol>>(txt);
            }
            UsuarioDTOViewModel vm = new UsuarioDTOViewModel();
            vm.Roles = roles;
            return View(vm);
        }

        [HttpPost]
        public ActionResult Login(UsuarioDTOViewModel vm)
        {
            HttpResponseMessage respuestaUsuario = Message(UrlUsuarios);
            List<DTORol> rolesUsuario = new List<DTORol>();
            List<DTOUsuario> usuarios = new List<DTOUsuario>();
            if (respuestaUsuario.IsSuccessStatusCode)
            {
                string txt = ObtenerBody(respuestaUsuario);
                usuarios = JsonConvert.DeserializeObject<List<DTOUsuario>>(txt);
            }
            bool encontrado = false;

            foreach ( DTOUsuario u in usuarios) 
            {

                if (vm.usuario.Email == u.Email && vm.usuario.Password == u.Password)
                {
                    foreach (DTOUsuarioRol ur in u.UsuarioRol)
                    {

                        HttpResponseMessage respuestaRol = Message(UrlRoles + "/" + ur.rolId);
                        if (respuestaRol.IsSuccessStatusCode)
                        {
                            string txt = ObtenerBody(respuestaRol);
                            DTORol rol = JsonConvert.DeserializeObject<DTORol>(txt);
                            rolesUsuario.Add(rol);
                        }
                    }
                }

                    foreach (DTORol rol in rolesUsuario)
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
                ViewBag.Error = "Rol no asignado a su usuario";
                HttpResponseMessage respuestaRoles = Message(UrlRoles);
                List<DTORol> roles = new List<DTORol>();
                if (respuestaRoles.IsSuccessStatusCode)
                {
                    string txt = ObtenerBody(respuestaRoles);
                    roles = JsonConvert.DeserializeObject<List<DTORol>>(txt);
                }

                vm.Roles = roles;
                return View(vm);
            }

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
