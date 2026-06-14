using Microsoft.AspNetCore.Mvc;

namespace ActividadPractica2.Controllers
{
    public class EstudiantesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
