using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Models;
using RentIT.Models.ViewModels;

namespace RentIT.Areas.Customer.Controllers
{
    [Area("Customer")]
	[Authorize]
    public class MyItemController : Controller
    {
        private readonly ILogger<MyItemController> _logger;
        private readonly IUnitOfWork _unitOfWork;
		private readonly IWebHostEnvironment _webHostEnvironment;
		private readonly UserManager<IdentityUser> _userManager;

        public MyItemController(ILogger<MyItemController> logger, IUnitOfWork unitOfWork, 
			UserManager<IdentityUser> userManager, IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
			_webHostEnvironment = webHostEnvironment;
        }
		[HttpGet]
        public IActionResult Index()
        {
            IEnumerable<Item> itemList = _unitOfWork.Item.GetAll().Where(i => i.CreatorId == _userManager.GetUserId(User));
            return View(itemList);
        }

		#region APICalls
		[HttpGet]
		public IActionResult GetAll()
		{
			List<Item> objItemList = _unitOfWork.Item.GetAll().Where(i => i.CreatorId == _userManager.GetUserId(User)).ToList();
			return Json(new { data = objItemList });
		}

		[HttpGet]
        public IActionResult Upsert(int? id)
        {
            ItemVM itemVM = new ItemVM()
            {
                Item = new Item()
            };

            if (id == null || id == 0)
            {
                return View(itemVM);
            }
            else
            {
                itemVM.Item = _unitOfWork.Item.Get(i => i.Id == id);
                return View(itemVM);
            }

        }

		[HttpPost]
		public async Task<IActionResult> Upsert(ItemVM obj, IFormFile? file)
		{
			if (ModelState.IsValid)
			{
				string wwRootPath = _webHostEnvironment.WebRootPath;
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string itemPath = Path.Combine(wwRootPath, @"img\item");

					if (!string.IsNullOrEmpty(obj.Item.ImageUrl))
					{
						var oldImgPath = Path.Combine(wwRootPath, obj.Item.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImgPath))
						{
							System.IO.File.Delete(oldImgPath);
						}
					}

					using (var fileStream = new FileStream(Path.Combine(itemPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					obj.Item.ImageUrl = @"\img\item\" + fileName;
				}

				if (obj.Item.Id == 0)
				{
					var creator = await _userManager.GetUserAsync(User);
					obj.Item.Creator = creator;
					_unitOfWork.Item.Add(obj.Item);
				}
				else
				{
					_unitOfWork.Item.Update(obj.Item);
				}

				_unitOfWork.Save();
				TempData["success"] = "Item added succesfully!";
				return RedirectToAction("Index");
			}
			return View(obj);
		}

		[HttpDelete]
		public IActionResult Delete(int? id)
		{
			var itemForDelete = _unitOfWork.Item.Get(i => id == i.Id);
			if (itemForDelete == null)
			{
				return Json(new { success = false, message = "Error while trying to delete item" });
			}

			var oldImgPath = Path.Combine(_webHostEnvironment.WebRootPath, itemForDelete.ImageUrl.TrimStart('\\'));
			if (System.IO.File.Exists(oldImgPath))
			{
				System.IO.File.Delete(oldImgPath);
			}

			_unitOfWork.Item.Remove(itemForDelete);
			_unitOfWork.Save();

			return Json(new { success = true, message = "Delete successful" });

		}

		#endregion
	}
}
