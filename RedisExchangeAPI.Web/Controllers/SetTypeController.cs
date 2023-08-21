using Microsoft.AspNetCore.Mvc;
using RedisExchangeAPI.Web.Services;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisExchangeAPI.Web.Controllers
{
    public class SetTypeController : Controller
    {
        private readonly RedisService _redisService;
        private readonly IDatabase _db;
        private string listKey = "setnames";
        public SetTypeController(RedisService redisService)
        {
            _redisService = redisService;
            _db = _redisService.GetDb(2);
        }
        public IActionResult Index()
        {
            HashSet<string> names = new();
            
            if(_db.KeyExists(listKey))
            {
                _db.SetMembers(listKey).ToList().ForEach(x =>
                {
                    names.Add(x.ToString());
                });
            }

            return View(names);
        }
        [HttpPost]
        public IActionResult Add(string name)
        {
            _db.KeyExpire(listKey, DateTime.Now.AddMinutes(5));

            _db.SetAdd(listKey, name);

            return RedirectToAction("Index");
        }
        [HttpGet]
        public async Task<IActionResult> Delete(string name)
        {
            await _db.SetRemoveAsync(listKey, name);
            return RedirectToAction("Index");
        }
    }
}
