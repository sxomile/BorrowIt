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
    public class ReportController : Controller
    {
        private readonly ILogger<ReportController> _logger;
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;
        public ReportController(ILogger<ReportController> logger, IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var reports = _unitOfWork.Report.GetAll().Where(r => r.AdminId == _userManager.GetUserId(User)
                || r.UserId == _userManager.GetUserId(User));
            return View(reports);
        }

        [HttpGet]
        public async Task<IActionResult> Create(string userId)
        {
            ApplicationUser user = (ApplicationUser)_unitOfWork.User.Get(u => u.Id == userId);
            Report report = new Report()
            {
                AdminId = _userManager.GetUserId(User),
                UserId = userId,
                Admin = (ApplicationUser)await _userManager.GetUserAsync(User),
                User = user,

            };

            ReportVM reportVM = new ReportVM()
            {
                User = user,
                Report = report,
            };

            return View(reportVM);

        }

        [HttpPost]
        public IActionResult Create(ReportVM obj)
        {
            try
            {
                _unitOfWork.Report.Add(obj.Report);
                _unitOfWork.Save();
                TempData["success"] = "Successful order!";
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return RedirectToAction("Index");
        }

        #region APICalls
        public IActionResult GetAll()
        {
            var objReportList = _unitOfWork.Report.GetAll(includeProperties: "Admin,User",
                filter: o => (o.AdminId == _userManager.GetUserId(User)
                || o.UserId == _userManager.GetUserId(User))
                   )
            .Select(r => new
            {
                r.Id,
                r.Description,
                AdminName = r.Admin.Name,
                UserName = r.User.Name
            })
            .ToList();

            return Json(new { data = objReportList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var reportForDelete = _unitOfWork.Report.Get(r => id == r.Id);
            if (reportForDelete == null)
            {
                return Json(new { success = false, message = "Error while trying to delete item" });
            }

            _unitOfWork.Report.Remove(reportForDelete);
            _unitOfWork.Save();

            return Json(new { success = true, message = "Delete successful" });

        }

        #endregion
    }
}
