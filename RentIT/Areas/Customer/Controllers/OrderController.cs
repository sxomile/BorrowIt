using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Models;
using RentIT.Models.ViewModels;

namespace RentIT.Areas.Customer.Controllers
{
	[Area("Customer")]
	[Authorize]
	public class OrderController : Controller
	{
		private readonly ILogger<OrderController> _logger;
		private readonly IUnitOfWork _unitOfWork;
		private readonly UserManager<IdentityUser> _userManager;

		public OrderController(ILogger<OrderController> logger, IUnitOfWork unitOfWork,
			UserManager<IdentityUser> userManager)
		{
			_logger = logger;
			_unitOfWork = unitOfWork;
			_userManager = userManager;
		}

		public IActionResult Index()
		{
			IEnumerable<Order> orders = _unitOfWork.Order.GetAll()
				.Where(o => o.LenderId == _userManager.GetUserId(User)
				|| o.BorrowerId == _userManager.GetUserId(User));
			return View(orders);
		}

		[HttpGet]
		public async Task<IActionResult> Create(int itemId)
		{
			Item item = _unitOfWork.Item.Get(i => i.Id == itemId, includeProperties: "Creator");
			Order order = new Order()
			{
				Borrower = await _userManager.GetUserAsync(User),
				BorrowerId = _userManager.GetUserId(User),
				Lender = item.Creator,
				LenderId = item.CreatorId,
				ItemName = item.Name,
				RentTime = item.RentTime,

			};

			OrderVM orderVM = new OrderVM()
			{
				Item = item,
				Order = order,
			};

			return View(orderVM);
		}

		[HttpPost]
		public IActionResult Create(OrderVM obj)
		{
			try
			{
				_unitOfWork.Item.Remove(obj.Item);
				obj.Order.Borrower = _unitOfWork.User.Get(u => u.Id == obj.Order.BorrowerId);
                obj.Order.Lender = _unitOfWork.User.Get(u => u.Id == obj.Order.LenderId);
                _unitOfWork.Order.Add(obj.Order);
				_unitOfWork.Save();
				TempData["success"] = "Successful order!";
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			return RedirectToAction("Index");
		}

		public IActionResult Details(int id)
		{
			Order order = _unitOfWork.Order.Get(expr: o => o.OrderId == id, includeProperties:"Lender,Borrower");
			return View(order);
		}

        #region APICalls
        public IActionResult GetAll()
        {
            List<Order> objItemList = _unitOfWork.Order.GetAll(includeProperties:"Lender,Borrower", 
				filter: (i => i.BorrowerId == _userManager.GetUserId(User) || i.LenderId == _userManager.GetUserId(User)))
				.ToList();
            return Json(new { data = objItemList });
        }

        #endregion


    }
}
