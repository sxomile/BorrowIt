using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using RentIT.DataAccess.Data;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Models;
using RentIT.Models.ViewModels;
using RentIT.Utility;

namespace RentIT.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = StaticDetails.Role_Admin)]
    public class ItemController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment; //da bismo direktno pristupali wwwroot-u
        private readonly UserManager<IdentityUser> _userManager;
        public ItemController(IUnitOfWork db, IWebHostEnvironment webHostEnvironment, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = db;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;

        }

        public IActionResult Index()
        {
            var objItemList = _unitOfWork.Item.GetAll().ToList();
            return View(objItemList);
        }

		public IActionResult Upsert(int? id)
		{
			var itemVM = new ItemVM()
			{
				Item = new Item(),
				CategoryList = _unitOfWork.ItemCategory.GetAll().ToList(),
				SelectedCategoryIds = new List<int>() // Initialize the list
			};

			if (id == null || id == 0)
			{
				return View(itemVM);
			}
			else
			{
				itemVM.Item = _unitOfWork.Item.Get(i => i.Id == id, includeProperties: "ItemItemCategories");

				// Populate SelectedCategoryIds with the related categories
				itemVM.SelectedCategoryIds = itemVM.Item.ItemItemCategories
					.Select(c => c.ItemCategoryId)
					.ToList();

				return View(itemVM);
			}
		}


		[HttpPost]
        public async Task<IActionResult> Upsert(ItemVM obj, IFormFile? file)
        {
			if (ModelState.IsValid)
			{
				string wwRootPath = _webHostEnvironment.WebRootPath;

				// Handle Image File Upload
				if (file != null)
				{
					string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
					string itemPath = Path.Combine(wwRootPath, @"img\item");

					// Delete old image if it exists
					if (!string.IsNullOrEmpty(obj.Item.ImageUrl))
					{
						var oldImgPath = Path.Combine(wwRootPath, obj.Item.ImageUrl.TrimStart('\\'));
						if (System.IO.File.Exists(oldImgPath))
						{
							System.IO.File.Delete(oldImgPath);
						}
					}

					// Save the new image
					using (var fileStream = new FileStream(Path.Combine(itemPath, fileName), FileMode.Create))
					{
						file.CopyTo(fileStream);
					}

					obj.Item.ImageUrl = @"\img\item\" + fileName;
				}

				// Handle creator if it's a new item
				if (obj.Item.Id == 0)
				{
					//Create new
					var creator = await _userManager.GetUserAsync(User);
					obj.Item.Creator = (ApplicationUser)creator;
					_unitOfWork.Item.Add(obj.Item);
					// Now add categories, even though the Item hasn't been saved yet.
					// EF Core will track the changes and handle this correctly during Save.

					// Handle Category Associations
					foreach (var categoryId in obj.SelectedCategoryIds)
					{
						var itemCategory = new ItemItemCategory
						{
							Item = obj.Item,  // Use the tracked Item object directly
							ItemCategoryId = categoryId
						};
						_unitOfWork.ItemItemCategory.Add(itemCategory);
					}

					// Handle New Categories
					if (obj.NewCategoryNames != null && obj.NewCategoryNames.Any() && obj.NewCategoryNames.Count > 0)
					{
						foreach (var newCategoryName in obj.NewCategoryNames)
						{

							List<string> newCategories = newCategoryName.Trim('[', ']', '\"').Split(',').ToList();

							if (newCategories.First().StartsWith("System."))
							{
								continue;
							}

							foreach (var category in newCategories)
							{
								var newCategory = new ItemCategory
								{
									Name = category.Replace("\"", "")
								};
								_unitOfWork.ItemCategory.Add(newCategory);

								var itemCategory = new ItemItemCategory
								{
									Item = obj.Item,
									ItemCategory = newCategory
								};

								_unitOfWork.ItemItemCategory.Add(itemCategory);
							}
						}
					}

					// Save everything in one go
					_unitOfWork.Save();

					TempData["success"] = "Item added successfully!";
					return RedirectToAction("Index");
				}
				else
				{
					//Update existing

					// Fetch the existing item
					var item = _unitOfWork.Item.Get(i => i.Id == obj.Item.Id);

					_unitOfWork.Item.Update(item);

					// Remove all existing categories for the item
					var existingItemCategories = _unitOfWork.ItemItemCategory.GetAll(iic => iic.ItemId == item.Id);
					foreach (var category in existingItemCategories)
					{
						_unitOfWork.ItemItemCategory.Remove(category);
					}

					// Add the newly selected categories
					foreach (var categoryId in obj.SelectedCategoryIds)
					{
						var itemCategory = new ItemItemCategory
						{
							ItemId = item.Id,  // Use the existing item's ID
							ItemCategoryId = categoryId
						};
						_unitOfWork.ItemItemCategory.Add(itemCategory);
					}

					// Handle New Categories
					if (obj.NewCategoryNames != null && obj.NewCategoryNames.Any())
					{
						foreach (var newCategoryName in obj.NewCategoryNames)
						{
							// Split new category names and trim any unwanted characters
							List<string> newCategories = newCategoryName.Trim('[', ']', '\"').Split(',').ToList();

							// Skip invalid categories
							if (newCategories.First().StartsWith("System."))
							{
								continue;
							}

							foreach (var category in newCategories)
							{
								var newCategory = new ItemCategory
								{
									Name = category.Replace("\"", "")
								};
								_unitOfWork.ItemCategory.Add(newCategory);

								var itemCategory = new ItemItemCategory
								{
									ItemId = item.Id,
									ItemCategory = newCategory
								};

								_unitOfWork.ItemItemCategory.Add(itemCategory);
							}
						}
						_unitOfWork.Item.Update(item);

						// Save all changes to the database
						_unitOfWork.Save();

						TempData["success"] = "Item updated successfully!";
						return RedirectToAction("Index");
					}
				}

			}

				return View(obj);
		}

        #region APICalls
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Item> objItemList = _unitOfWork.Item.GetAll().ToList();
            return Json(new {data =  objItemList});
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var itemForDelete = _unitOfWork.Item.Get(i => id == i.Id);
            if(itemForDelete == null)
            {
                return Json(new {success = false, message = "Error while trying to delete item"});
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
