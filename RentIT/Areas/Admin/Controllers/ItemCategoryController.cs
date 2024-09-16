using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace RentIT.Areas.Admin.Controllers
{
	[Area("Admin")]
	[Authorize]
	public class ItemCategoryController : Controller
	{
		public IActionResult Index()
		{

			return View();
		}
	}
}
