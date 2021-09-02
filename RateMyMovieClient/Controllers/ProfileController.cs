using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using RateMyMovieClient.Models;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;

namespace RateMyMovieClient.Controllers
{
    public class ProfileController : Controller
    {
        public IActionResult Index()
        {
            if ((HttpContext.Session.GetInt32("IsLogin")) == 1)
            {
                ViewBag.session = HttpContext.Session.GetString("Username");
            }

            return View();

        }
    }
}
