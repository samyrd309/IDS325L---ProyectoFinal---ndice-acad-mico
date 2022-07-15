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

        public ActionResult Help()
        {
            return View();
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


        public ActionResult CambiarContraseña()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CambiarContraseña(int Matricula, string Email, string Contraseña, string Confirmacion)
        {
            var usuario = _indiceContext.Personas.FirstOrDefault(u => u.Matricula == Matricula && u.CorreoElectronico == Email);

            if (usuario == null)
            {
                ViewBag.Message = string.Format("El usuario no existe en el sistema");
                return View(); // Error de campos vacíos
            }

            if (Contraseña != Confirmacion)
            {
                ViewBag.Message = string.Format("La contraseña es diferente a la confirmación");
                return View();  // Vista o mensaje de error
            }// Manejar mecanismo de validación de nueva contraseña


            usuario.Contraseña = Contraseña;
            _indiceContext.Personas.Update(usuario);
            _indiceContext.SaveChanges();

            return View("Login");
        }


        public ActionResult Privacy()
        {
            ViewBag.test = new List<Literal>()
            {
                new Literal() {Nota = "G", Numero = (decimal?)1.6},
                new Literal() {Nota = "H", Numero = (decimal?)1.9}
            };
            
            return View();
        }
    }
}