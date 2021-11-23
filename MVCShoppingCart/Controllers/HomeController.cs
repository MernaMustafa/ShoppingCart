using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCShoppingCart.Models;
using MVCShoppingCart.Services;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace MVCShoppingCart.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _client;
        private readonly HttpService<User> _httpService;
        private readonly SessionService _sessionService;

        public HomeController(HttpClient client, SessionService sessionService)
        {
            _client = client;
            _httpService = new HttpService<User>(_client);
            _sessionService = sessionService;

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(User user)
        {
            if (ModelState.IsValid)
            {
                bool res = await _httpService.HttpPost("users", user);

                if (res)
                {
                    //_sessionService.SetSession(user);
                    return RedirectToAction("Index", "Customer");
                }
            }
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthenticateModel authenticateModel)
        {
            //User user = new User();
            string data = JsonConvert.SerializeObject(authenticateModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("auth", content);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse = response.Content.ReadAsStringAsync().Result;
                UserInfoModel userInfo = JsonConvert.DeserializeObject<UserInfoModel>(apiResponse);
                _sessionService.SetSession(userInfo);
                if (userInfo.Role == "admin")
                {
                    return RedirectToAction("Index", "Item");
                }
                else
                {
                    return RedirectToAction("Index", "Customer");
                }
            }
           
            return View();
        }

        

        public IActionResult Logout()
        {
            _sessionService.ClearSession();
            return RedirectToAction("Index");
        }

        public IActionResult Error()
        {
            return View();
        }


    }
}
