﻿using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IndiceContext _indiceContext;

        public HomeController(ILogger<HomeController> logger, IndiceContext indiceContext)
        {
            _logger = logger;
            _indiceContext = indiceContext;
        }

        public IActionResult Index()
        {
            return View();
        }

        public static Persona usuario = new Persona();

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Help()
        {
            return View();
        }


        //POST: Usuario
        [HttpPost]
        public async Task<IActionResult> Login(int? Matricula, string Contraseña)
        {

            if (ModelState.IsValid)
            {
                if(Matricula == null || Contraseña == null)
                {
                    ViewBag.Message = string.Format("Necesita llenar todos los campos del formulario");
                    return View();
                }

                usuario = _indiceContext.Personas.FirstOrDefault(u => u.Matricula == Matricula && u.Contraseña == Contraseña);
                //usuario = await _indiceContext.Personas.FindAsync(Matricula, Contraseña);

                if(_indiceContext.Personas.FirstOrDefault(u => u.Matricula == Matricula) == null)
                {
                    ViewBag.Message = string.Format("La Matricula registrada no existe, debe ser ingresado al sistema");
                    return View();
                }

                if(usuario == null)
                {
                    ViewBag.Message = string.Format("La contraseña es inválida");
                    return View();
                }
               
                if (usuario != null)
                {

                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, usuario.Nombre),
                        new Claim("Matricula", usuario.Matricula.ToString()),
                        new Claim(ClaimTypes.Role, usuario.IdRol.ToString())
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                    
                    if (usuario.IdRol == 1)
                    {
                        return RedirectToAction("IndexEstudiantes", "Persona", usuario);
                    }
                    else if (usuario.IdRol == 2)
                    {
                        return RedirectToAction("Index", "CalificacionesEstudiantes", usuario);
                    }
                    else if (usuario.IdRol == 3)
                    {
                        //ViewBag["MatriculaUsuario"] = Matricula;
                        return RedirectToAction("IndexAsignaturasDocente", "Calificacion", usuario);
                    }
                    else
                    {
                        return RedirectToAction("Login", "Home");
                    }
                    
                }
                else
                {
                    TempData["InvalidUser"] = "Bienvenido";
                    return RedirectToAction("Login", "Home");
                }

            }
            return RedirectToAction("Login", "Home");


        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
        }


        public ActionResult CambiarContraseña()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarContraseña(int? Matricula, string? Email, string? Contraseña, string? Confirmacion)
        {
            if(Matricula == null || Email == null || Contraseña == null || Confirmacion == null)
            {
                ViewBag.Message = string.Format("Necesita llenar todos los campos del formulario");
                return View();
            }

            var usuario = _indiceContext.Personas.FirstOrDefault(u => u.Matricula == Matricula && u.CorreoElectronico == Email);

            if (usuario == null)
            {
                ViewBag.Message = string.Format("El usuario no existe en el sistema");
                return View();
            }

            if (Contraseña != Confirmacion)
            {
                ViewBag.Message = string.Format("La contraseña es diferente a la confirmación");
                return View();  
            }



            usuario.Contraseña = Contraseña;
            _indiceContext.Personas.Update(usuario);
            _indiceContext.SaveChanges();

            return View("Login");
        }

    }
}