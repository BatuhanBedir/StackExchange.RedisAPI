using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;

namespace RedisExchangeAPI.Web.Controllers
{
    public class HashTypeController : BaseController
    {
        public string Key { get; set; } = "dictionary";
        public HashTypeController(RedisService redisService) : base(redisService)
        {
        }
        
        public IActionResult Index()
        {
            Dictionary<string, string> list = new();
            if(_db.KeyExists(Key))
            {
                _db.HashGetAll(Key).ToList().ForEach(x =>
                {
                    list.Add(x.Name, x.Value);
                });
            }
            return View(list);
        }
        [HttpPost]
        public IActionResult Add(string name, string value)
        {
            _db.HashSet(Key, name, value);

            return RedirectToAction("Index");
        }

        public IActionResult Delete(string name)
        {
            _db.HashDelete(Key, name);

            return RedirectToAction("Index");
        }
    }
}
