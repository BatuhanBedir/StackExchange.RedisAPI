using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;

namespace RedisExchangeAPI.Web.Controllers
{
    public class StringTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase db;
        public StringTypeController(RedisService redisService)
        {
            _redisService = redisService;
            db = _redisService.GetDb(0);
        }

        public IActionResult Index()
        {
            db.StringSet("name", "Batuhan");
            db.StringSet("visitor",100);
            return View();
        }
        public IActionResult Show()
        {
            var value = db.StringLength("name");
            //var value =db.StringGet("name");
            //var value = db.StringGetRange("name", 0, 3);
            //db.StringIncrement("visitor",1);

            //var visitor = db.StringDecrementAsync("visitor", 1).Result;
            //db.StringDecrementAsync("visitor", 10).Wait();

            //if(value.HasValue)
            //{
            //    ViewBag.value = value.ToString();
            //}
            ViewBag.value = value.ToString();
            return View();
        }
    }
}
