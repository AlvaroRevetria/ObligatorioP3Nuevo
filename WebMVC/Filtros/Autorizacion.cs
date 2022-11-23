using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebMVC.Filtros
{
    public class Autorizacion : Attribute, IAuthorizationFilter
    {
        public string[] Roles { get; set; }

        public Autorizacion(params string[] roles)
        {
            Roles = roles;
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            string rolUsuario = context.HttpContext.Session.GetString("rol");

            if (rolUsuario == null || !Roles.Any(rol => rol == rolUsuario)) // !Roles.Contains(rolUsuario)
            {
                context.Result = new RedirectToActionResult("login", "usuarios", null);
            }
        }
    }
}