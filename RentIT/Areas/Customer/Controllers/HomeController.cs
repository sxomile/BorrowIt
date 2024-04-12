using Microsoft.AspNetCore.Mvc;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Models;
using System.Diagnostics;

namespace RentIT.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> itemList = _unitOfWork.Item.GetAll();
            return View(itemList);
        }

        public IActionResult Details(int id)
        {
            Item item = _unitOfWork.Item.Get(i => i.Id == id, includeProperties: "Creator");
            return View(item);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}