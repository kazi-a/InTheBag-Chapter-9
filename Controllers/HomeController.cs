using InTheBag.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace InTheBag.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult IndexViewBag()
        {
            IList<string> wishList = new List<string>
            {
                "Peace",
                "Health",
                "Happiness"
            };

            ViewBag.WishList = wishList;
            return View();
        }

        public ActionResult IndexViewData()
        {
            IList<string> wishList = new List<string>
            {
                "Quies",
                "Salutem",
                "Beatitudinem"
            };

            ViewData["WishList"] = wishList;
            return View();
        }

        public IActionResult IndexTempData()
        {
            IList<string> wishList = new List<string>
            {
                "La Paz",
                "La Salud",
                "La Felicidad"
            };

            TempData["WishList"] = wishList;
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        public ActionResult WishIndex()
        {
            string myWishList = HttpContext.Session.GetString("wish");
            Wishes myWishes = myWishList != null ? JsonSerializer.Deserialize<Wishes>(myWishList) : new Wishes();

            return View(myWishes);
        }

        public IActionResult NewWishIndex() 
        {
            return View();
        }

        [HttpPost]
        public ActionResult NewWishIndex(Wishes model) 
        {
            Wishes myWishes = new Wishes
            {
                ID = 2,
                Wish1 = model.Wish1,
                Wish2 = model.Wish2,
                Wish3 = model.Wish3
            };

            string jsonWishes = JsonSerializer.Serialize(myWishes);
            HttpContext.Session.SetString("wish", jsonWishes);
            return RedirectToAction("WishIndex");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
