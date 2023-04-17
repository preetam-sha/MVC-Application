using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Services.Description;
using System.Web.UI.WebControls;
using Cars_Inventory.Models;
using log4net;
using Newtonsoft.Json;

namespace Cars_Inventory.Controllers
{
    public class CarsInventoryController : Controller
    {
        private ILog Logger = LogManager.GetLogger(typeof(CarsInventoryController));

        Uri baseAddress = new Uri("https://localhost:44399");
        HttpClient client;

        public CarsInventoryController()
        {
        }

        public async Task<ActionResult> Index()
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            List<Cars> cars = new List<Cars>();
            HttpResponseMessage response = await client.GetAsync("api/CarInventoryApi");
            if (response.IsSuccessStatusCode)
            {
                string data = response.Content.ReadAsStringAsync().Result;
                cars = JsonConvert.DeserializeObject<List<Cars>>(data);
            }
            return View(cars);
        }
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Cars cars)
        {
            client = new HttpClient();
            client.BaseAddress = baseAddress;
            string data = JsonConvert.SerializeObject(cars);
            var message = new HttpRequestMessage
            {
                Content = new StringContent(data, Encoding.UTF8, "application/json")
            };
            message.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response = await client.PostAsync("api/CarInventoryApi", message.Content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return View();
        }
        public async Task<ActionResult> Edit(int id = 0)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399");
                HttpResponseMessage response = await client.GetAsync($"api/CarInventoryApi/"+id);
 
                if (!response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Edit");
                }
                var json = response.Content.ReadAsStringAsync().Result;
                var result = JsonConvert.DeserializeObject<Cars>(json);
                return View(result);
            }
        }
        [HttpPost]
        public async Task<ActionResult> Edit(Cars cars)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399");

                var data = JsonConvert.SerializeObject(cars);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync($"api/CarInventoryApi/{cars.Id}", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

                var errorMessage = await response.Content.ReadAsStringAsync();
                ModelState.AddModelError("", errorMessage);
                return View(cars);
            }
        }
         
        public async Task<ActionResult> Delete(int id)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399");

                HttpResponseMessage response = await client.DeleteAsync($"api/CarInventoryApi/{id}");
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    return Content(errorMessage);
                }
            }
        }

        public ActionResult SearchByModelAndBrand(string Model, string brand)
        {
            var url = $"api/myendpoint?model={Model}&brand={brand}";
            var response = client.GetAsync(url).Result;
            response.EnsureSuccessStatusCode();

            var json = response.Content.ReadAsStringAsync().Result;
            var result = JsonConvert.DeserializeObject<List<Cars>>(json);

            return View(result);
        }

        public async Task<ActionResult> Signup(Signup signup)
        {
            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:44399");

                var data = JsonConvert.SerializeObject(signup);
                var content = new StringContent(data, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("api/CarInventoryApi", content);

                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index", "CarsInventory");
                }
                else
                {
                    var errorMessage = await response.Content.ReadAsStringAsync();
                    ModelState.AddModelError("", errorMessage);
                    return View(signup);
                }
            }
        }


    }
}