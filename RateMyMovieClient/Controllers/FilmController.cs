using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using RateMyMovieClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net;
using System.Web;
using Newtonsoft.Json;

namespace RateMyMovieClient.Controllers
{
    public class FilmController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:5000";
        public async Task<ActionResult> Index(string id)
        {
            MovieSet movieSetsInfo = new MovieSet();

            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/movies/" + id);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var MovResponse = Res.Content.ReadAsStringAsync().Result;

                    //Deserializing the response recieved from web api and storing into the MovieSet list  
                    movieSetsInfo = JsonConvert.DeserializeObject<MovieSet>(MovResponse);

                }
                //returning the employee list to view  
                return View(movieSetsInfo);
            }
        }
        public async Task<ActionResult> AddMovie()
        {
            return View();
        }
        [HttpPost]
        [ActionName("CreateMovie")]
        public async Task<ActionResult> CreateMovieAsync(MovieSet movieSet)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "/movies", movieSet);

                return RedirectToAction("Index", "Home");
            }
        }
    }
}
