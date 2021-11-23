using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using MVCShoppingCart.Models;
using MVCShoppingCart.Services;
using Newtonsoft.Json;

namespace MVCShoppingCart.Controllers
{

    [Authorize]
    [TypeFilter(typeof(AdminFilter))]
    public class UserController : Controller
    {
        private readonly HttpClient _client;
        private readonly HttpService<User> _httpService;
        private readonly SessionService _sessionService;

        public UserController(HttpClient client, SessionService sessionService)
        {
            _client = client;
            _sessionService = sessionService;
            _httpService = new HttpService<User>(_client);
            _httpService.SetAuthorizationHeader(_sessionService.GetToken());

        }
        public async Task<IActionResult> Index()
         {
            return View(await _httpService.HttpGetList("users"));
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(User user)
        {
            bool res = await _httpService.HttpPost("users", user);
            if (res)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _httpService.HttpDelete("users" + "/" + id.ToString());
            if (res)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }

}