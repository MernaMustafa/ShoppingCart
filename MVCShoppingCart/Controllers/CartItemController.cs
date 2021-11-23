using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using MVCShoppingCart.Models;
using MVCShoppingCart.Services;
using Newtonsoft.Json;

namespace MVCShoppingCart.Controllers
{
    [Authorize]
    public class CartItemController : Controller
    {
        private readonly HttpClient _client;
        private readonly HttpService<CartItem> _httpService;
        private readonly SessionService _sessionService;

        public CartItemController(HttpClient client, SessionService sessionService)
        {
            _client = client;
            _sessionService = sessionService;
            _httpService = new HttpService<CartItem>(_client);
            _httpService.SetAuthorizationHeader(_sessionService.GetToken());

        }

        public async Task<IActionResult> Index(int id)
        {
            var res = await _httpService.HttpGetList("cartitems" + "/" + id.ToString());
            
            return View(res);
        }

        [HttpPost]
        public async Task<IActionResult> AddCartItem(OrderItemViewModel OrderItemViewModel)
        {
            string data = JsonConvert.SerializeObject(OrderItemViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("cartitems", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index", new { id = HttpContext.Session.GetString("UserID") });
            }
            return RedirectToAction("Index", new { id = HttpContext.Session.GetString("UserID") });
        }


        [HttpPost]
        public async Task<string> Update(UpdateViewModel updateViewModel)
        {
            string data = JsonConvert.SerializeObject(updateViewModel);
            StringContent content = new StringContent(data, Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _client.PostAsync("cartitems/update", content);
            //bool res = await _httpService.HttpPost(uri, updateViewModel);
            if (response.IsSuccessStatusCode)
            {
                string apiResponse =  await response.Content.ReadAsStringAsync();
                System.Diagnostics.Debug.WriteLine("------------------------" + apiResponse);
                //string result = "";
                /*if (response.Headers.TryGetValues("result", out IEnumerable<string> headerValues))
                {
                    result = headerValues.FirstOrDefault();
                }*/
                return apiResponse;
            }
             return "Error";
            //RedirectToAction("Index", "CartItem", new { id = HttpContext.Session.GetString("UserID") })
        }

    }
}