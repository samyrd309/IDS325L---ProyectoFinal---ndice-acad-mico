using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
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

        public Task<ViewResult> Login()
        {
            return Task.FromResult(View());
        }

        //POST: Usuario
        [HttpPost]
        public async Task<IActionResult> IniciarSesion(int Matricula, string Contraseña)
        {

            if (ModelState.IsValid)
            {
                usuario = _indiceContext.Personas.FirstOrDefault(u => u.Matricula == Matricula && u.Contraseña == Contraseña);
                //usuario = await _indiceContext.Personas.FindAsync(Matricula, Contraseña);

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

                    return RedirectToAction("Index", "CalificacionesEstudiantes", usuario);
                }
                else
                {
                    return RedirectToAction("Login", "Home");
                }

            }
            return RedirectToAction("Login", "Home", usuario);


        }

        public async Task<IActionResult> CerrarSesion()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            return RedirectToAction("Login", "Home");
        }

    }
}