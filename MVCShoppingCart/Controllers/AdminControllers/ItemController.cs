using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCShoppingCart.Models;
using MVCShoppingCart.Services;

namespace MVCShoppingCart.Controllers
{

    [Authorize]
    [TypeFilter(typeof(AdminFilter))]
    public class ItemController : Controller
    {
        private readonly HttpClient _client;
        private readonly HttpService<Item> _httpService;
        private readonly SessionService _sessionService;
        public ItemController(HttpClient client, SessionService sessionService)
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

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Item item)
        {
            bool res = await _httpService.HttpPost("items", item);
            if (res)
            {
                return RedirectToAction("Index");
            }
                return View();
        }

       
        public async Task<IActionResult> Edit(int id)
        {
            
            return View(await _httpService.HttpGet("items" + "/" + id.ToString()));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Item item)
        {
            
            bool res = await _httpService.HttpPut("items", item);
            
            if (res)
            {
                return RedirectToAction("Index");
            }
            return View();
        }

        public async Task<IActionResult> Delete(int id)
        {
            bool res = await _httpService.HttpDelete("items" + "/" + id.ToString());
            if (res)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
    }
}