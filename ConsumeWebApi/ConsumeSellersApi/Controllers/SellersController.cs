using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ConsumeSellersApi;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using ConsumeSellersApi.Models;

namespace ConsumeSellersApi.Controllers
{



    public class SellersController : Controller
    {
        //API URL Location 
        string BaseUrl = "https://localhost:44392/";

        // GET: Sellers
        public async Task<ActionResult> Index()
        {

            List<sellers> vendInfo = new List<sellers>();

            using (var seller = new HttpClient())
            {

                seller.BaseAddress = new Uri(BaseUrl);
                seller.DefaultRequestHeaders.Clear();
                seller.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Call all sellers using HttpClient
                HttpResponseMessage Res = await seller.GetAsync("api/vendedores");

                if (Res.IsSuccessStatusCode)
                {
                    //If Res is true get into and assign data 
                    var vendResponse = Res.Content.ReadAsStringAsync().Result;
                    //deserialize api and save data 
                    vendInfo = JsonConvert.DeserializeObject<List<sellers>>(vendResponse);


                }

                // Display all sellers list
                return View(vendInfo);
            }
        }

        // Creates a new seller 
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(sellers _sellers)
        {
            
            _sellers.CODIGO_CIUDAD = (int)_sellers._city;

            using (var vend = new HttpClient())
            {
                vend.BaseAddress = new Uri("https://localhost:44392/Api/vendedores/");
                var postTask = vend.PostAsJsonAsync<sellers>("sellers", _sellers);
                postTask.Wait();
                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            ModelState.AddModelError(string.Empty, "Error, contacta al administrador. ");
            return View(_sellers);



        }


        //Modify seller

        public ActionResult Edit(int id)
        {
            //Models

            sellers _sellers = null;

            using (var vend = new HttpClient())
            {
                vend.BaseAddress = new Uri(BaseUrl);
                //HTTP GET

                var responseTask = vend.GetAsync("api/vendedores/" + id.ToString());
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<sellers>();
                    readTask.Wait();
                    _sellers = readTask.Result;
                }
            }
            return View(_sellers);

        }

        [HttpPost]
        public ActionResult Edit(sellers _sellers)
        {
            _sellers.CODIGO_CIUDAD = (int)_sellers._city;

            using (var vend = new HttpClient())
            {
                vend.BaseAddress = new Uri(BaseUrl);

                //HTTP POST
                var putTask = vend.PutAsJsonAsync($"api/vendedores/{_sellers.CODIGO }", _sellers);
                putTask.Wait();
                var result = putTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");

                }
            }
            return View(_sellers);

        }

        public ActionResult Delete(int id)
        {
            sellers _sellers = null;
            using (var vend = new HttpClient())
            {
                vend.BaseAddress = new Uri(BaseUrl);

                //HTTP GET

                var respnseTask = vend.GetAsync("api/vendedores/" + id.ToString());
                respnseTask.Wait();
                var result = respnseTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<sellers>();
                    readTask.Wait();
                    _sellers = readTask.Result;
                }
            }
            return View(_sellers);

        }

        [HttpPost]
        public ActionResult Delete(sellers _sellers, int id)
        {
            using (var vend = new HttpClient())
            {
                vend.BaseAddress = new Uri(BaseUrl);

                //HTTP DELETE
                var deleteTask = vend.DeleteAsync($"api/vendedores/" + id.ToString());
                deleteTask.Wait();
                var result = deleteTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }

            }
            return View(_sellers);

        }

    }
}