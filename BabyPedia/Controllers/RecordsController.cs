using Microsoft.AspNetCore.Mvc;

namespace BabyPedia.Controllers
{
    public class RecordsController : Controller
    {
        public IActionResult RecordPatient()
        {
            return View();
        }
    }
}
