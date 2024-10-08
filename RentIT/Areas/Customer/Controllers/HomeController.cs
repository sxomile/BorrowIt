﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RentIT.DataAccess.Repository.IRepository;
using RentIT.Helpers;
using RentIT.Models;
using RentIT.Models.ViewModels;
using System.Diagnostics;
using System.Globalization;
using System.Net;
using X.PagedList;
using X.PagedList.Mvc;

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

        public IActionResult Index(int? page, bool nearby = false, string search = "")
        {
            HomeVM homeVM = new HomeVM();
            homeVM.Nearby = nearby;
			ViewData["CurrentSearch"] = search;

			if (!nearby)
            {
                homeVM.Items = _unitOfWork.Item.GetAll(i => i.Name.StartsWith(search), includeProperties: "Creator,Creator.CityOfResidence,Creator.CityOfResidence.Country,Creator.CityFromID,Creator.CityFromID.Country").ToPagedList(page ?? 1, 8);
                return View(homeVM);

			}

			var ipInfo = new IPInfo();

			try
			{
				var url = "https://ipinfo.io?token=bab16e3d59992a";
				var info = new WebClient().DownloadString(url);
				ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
				RegionInfo myRI = new RegionInfo(ipInfo.Country);
				ipInfo.Country = myRI.EnglishName;  //onaj JSON je samo sifra od 2 slova
													//zato mi treba ovako nesto da konverujem u pravo ime drzave pa da s tim mogu da radim nes pametno
				homeVM.Country = ipInfo.Country;
				homeVM.City = ipInfo.City;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

			homeVM.Items = _unitOfWork.Item.GetAll(i => ((ApplicationUser)i.Creator).CityOfResidence.Name == homeVM.City
			    && ((ApplicationUser)i.Creator).CityOfResidence.Country.CountryName == homeVM.Country
                && i.Name.StartsWith(search), 
                includeProperties: "Creator,Creator.CityOfResidence,Creator.CityOfResidence.Country,Creator.CityFromID,Creator.CityFromID.Country")
                .ToPagedList(page ?? 1, 8);
			

            //sada je izmenjeno i deluje da je dobro, ali pratiti vremenom da ne dodje do neke greske
            return View(homeVM);
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