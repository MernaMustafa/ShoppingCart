using Microsoft.AspNetCore.Mvc;
using MVCShoppingCart.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.AspNetCore.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using MVCShoppingCart.Services;

namespace MVCShoppingCart.Controllers
{
    [Authorize]
    public class CustomerController : Controller
    {
        private readonly HttpClient _client;
        private readonly HttpService<Item> _httpService;
        private readonly SessionService _sessionService;
        public CustomerController(HttpClient client, SessionService sessionService)
        {
            _client = client;
            _sessionService = sessionService;
            _httpService = new HttpService<Item>(_client);
            _httpService.SetAuthorizationHeader(_sessionService.GetToken());
        }
        public async Task<IActionResult> Index()
        {
            return View(await _httpService.HttpGetList("items"));
        }
    }
}
