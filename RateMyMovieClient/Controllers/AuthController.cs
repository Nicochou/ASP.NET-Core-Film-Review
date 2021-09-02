using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Session;
using Microsoft.Extensions.Logging;
using RateMyMovieClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;


namespace RateMyMovieClient.Controllers
{
    public class AuthController : Controller
    {
        //Hosted web API REST Service base url  
        string Baseurl = "http://localhost:5000";
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ActionName("LoginUser")]
        public async Task<ActionResult> GetUserAsync(UserSet userSet)
        {
            UserSet userSetsInfo = new UserSet();
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                //Sending request to find web api REST service resource GetAllEmployees using HttpClient  
                HttpResponseMessage Res = await client.GetAsync("/users/" + userSet.Username);

                //Checking the response is successful or not which is sent using HttpClient  
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api   
                    var MovResponse = Res.Content.ReadAsStringAsync().Result;
                    Console.Write(MovResponse);

                    //Deserializing the response recieved from web api and storing into the MovieSet list  
                    userSetsInfo = JsonConvert.DeserializeObject<UserSet>(MovResponse);
                    //HttpContext.Session.SetString("Username", userSet.Username);
                    //HttpContext.Session.SetInt32("IsLogin", 1);
                    return RedirectToAction("Index", "Profile");
                }
                else
                {
                    //HttpContext.Session.SetInt32("IsLogin", 0);
                    return RedirectToAction("Login");
                }
            }
        }

        [HttpPost]
        [ActionName("RegisterUser")]
        public async Task<ActionResult> CreateUserAsync(UserSet userSet)
        {
            using (var client = new HttpClient())
            {
                //Passing service base url  
                client.BaseAddress = new Uri(Baseurl);

                client.DefaultRequestHeaders.Clear();
                //Define request data format  
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage response = await client.PostAsJsonAsync(
                    "/users", userSet);
                if (response.IsSuccessStatusCode)
                {
                    HttpContext.Session.SetString("Username", userSet.Username);
                    HttpContext.Session.SetInt32("IsLogin", 1);
                    return RedirectToAction("Index", "Profile", new { userSet = userSet });
                }
                else
                {
                    HttpContext.Session.SetInt32("IsLogin", 0);
                    return RedirectToAction("Register");
                }
                    
            }
        }
    }
}
