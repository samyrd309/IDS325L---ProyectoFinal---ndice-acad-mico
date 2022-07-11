using IDS325L___ProyectoFinal___Índice_académico.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace IDS325L___ProyectoFinal___Índice_académico.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        
    }
}