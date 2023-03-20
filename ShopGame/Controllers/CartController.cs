using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using ShopGame.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ShopGame.Controllers
{

    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
