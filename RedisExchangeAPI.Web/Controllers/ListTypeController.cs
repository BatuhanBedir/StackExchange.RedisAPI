using Microsoft.AspNetCore.Mvc;

namespace RedisExchangeAPI.Web.Controllers
{
    public class ListTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
