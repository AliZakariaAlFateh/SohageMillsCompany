using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Security.Claims;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Sohag__Mills_Company.Controllers
{
    public class SectorController : Controller
    {

        IRepository<Sector> repostory;
        private readonly ECcontext db;
        IRepository<Region_Sector_Details> repoArea;
        IRepository<PhonesSectors> repophone;
        IRegionRepo regionRepo;
        IPhonesRepo phonesRepo;
        private Sector Sectors { get; }
        //private IProduct Product;
        public SectorController(IRepository<Sector> _repostory, ECcontext _db,
            IRepository<Region_Sector_Details> repoArea, IRepository<PhonesSectors> repophone , IRegionRepo regionRepo,IPhonesRepo phonesRepo)
        {
            this.repophone = repophone;
            this.repoArea = repoArea;
            repostory = _repostory;
            db = _db;
            this.regionRepo = regionRepo;
            this.phonesRepo = phonesRepo;
        }
        public IActionResult IndexSectors()
        {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)repostory.GetAll().Count() / 5m);
            //IEnumerable<Shareholder_Structure> Data = repostory.GetAll();
            //ViewBag.Cat = repostory.GetAll();
            var Sectors_D = repostory.GetAll();
            //"../DashBord/Index"
            return View(Sectors_D);

        }

        public IActionResult IndexArea()
        {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)regionRepo.GetAll().Count() / 5m);
            //IEnumerable<Shareholder_Structure> Data = repostory.GetAll();
            //ViewBag.Cat = repostory.GetAll();
            var AllArea = regionRepo.GetAll();
            //"../DashBord/Index"
            return View(AllArea);

        }
        public IActionResult IndexPhones()
        {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)phonesRepo.GetAll().Count() / 5m);
            //IEnumerable<Shareholder_Structure> Data = repostory.GetAll();
            //ViewBag.Cat = repostory.GetAll();
            var Sectors_D = phonesRepo.GetAll();
            //"../DashBord/Index"
            return View(Sectors_D);

        }


        public IActionResult GetAllSectors(int pageNumber, int pageSize = 5)
        {
            var Sectors = repostory.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/Sector/Partial/_TeamsTables.cshtml", Sectors);
        }

        public IActionResult GetAllAreas(int pageNumber, int pageSize = 5)
        {
            var Areas = regionRepo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/Sector/Partial/_AreaTables.cshtml", Areas);
        }
        public IActionResult GetAllPhones(int pageNumber, int pageSize = 5)
        {
            var Phones = phonesRepo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/Sector/Partial/_PhonesTables.cshtml", Phones);
        }

        public IActionResult GetAll_Sectors_With_Region_phones()
        {
            var All_sector_region_phones = phonesRepo.GetAll_Sectors_With_Region_phones();
       /*     var options = new JsonSerializerOptions
            {
                ReferenceHandler = ReferenceHandler.Preserve,
                // Other serialization options if needed
            };

            var json = JsonSerializer.Serialize(All_sector_region_phones, options);
       */
           // return Json(new { res = json });
            return View(All_sector_region_phones);
        }



        [HttpGet]
        public JsonResult GetAllForShow()
        {
            var member = repostory.GetAll();
            return Json(new { member });
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")] create sector
        public IActionResult Create(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "اضافة قطاع";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل نبذة عن الشركة";
                Sector sector = repostory.GetByID(id);
                return View(sector);
            }

        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        public IActionResult Create(Sector sector)
        {
            if (sector.Id == 0 && ModelState.IsValid)
            {
                repostory.Add(sector);
                repostory.Save();
                return RedirectToAction("IndexSectors");

            }
            else if (sector.Id > 0 && ModelState.IsValid)
            {
                Sector currentsector = repostory.GetByID(sector.Id);
                if (currentsector != null)
                {
                    currentsector.Sector_Name = sector.Sector_Name;


                    repostory.Update(currentsector);
                    repostory.Save();
                    return RedirectToAction("IndexSectors");

                }
            }
            return View(sector);
        }


        [HttpGet]
        public JsonResult Details(int id)
        {
            var member = repostory.GetByID(id);
            return Json(new { member });
        }
        [HttpGet]
        public JsonResult DetailsArea(int id)
        {
            var member = repoArea.GetByID(id);
            return Json(new { member });
        }
        public JsonResult DetailsPhone(int id)
        {
            var member = repophone.GetByID(id);
            return Json(new { member });
        }
        //create area
        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult CreateArea(int id, int sector_id)
        {
            if (id == 0 )
            {
                ViewData["title"] = "إضافة المنطقة";
                ViewBag.sectorid = sector_id;
                return View();
            }
            else
            {
                ViewData["title"] = " تعديل المنطقة";
                ViewBag.sectorid = sector_id;
                var Region = repoArea.GetByID(id);
                return View(Region);
            }

        }
        //create area

        [HttpPost]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreateArea(Region_Sector_Details? regionVM)
        {
            if (ModelState.IsValid && regionVM.Id == 0)
            {
                try
                {
                    //need to id sector
                    repoArea.Add(regionVM);
                    repoArea.Save();
                    return RedirectToAction("IndexArea");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "area is invalid " + ex.Message);
                }
            }
            else if (regionVM.Id > 0 && ModelState.IsValid)
            {
                var oldObj = repoArea.GetByID(regionVM.Id);
                if (oldObj != null)
                {
                    oldObj.Area_Name = regionVM.Area_Name;
                    oldObj.Address_Name = regionVM.Address_Name;
                    oldObj.SectorId = regionVM.SectorId;
                    repoArea.Update(oldObj);
                    repoArea.Save();
                }
                return RedirectToAction("IndexArea");

            }
            return View(regionVM);  
        }



        //delete sector
        public IActionResult Delete(int id)
        {
            try
            {
                repostory.Delete(id);
                int result = repostory.Save();

                if (result > 0)
                {
                    // Return a JSON success response
                    return Json(new { success = true, message = "Member deleted successfully." });
                }
                else
                {
                    // Return a JSON error response
                    return Json(new { success = false, message = "Failed to delete member." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while deleting the member." });
            }
        }
        //delete area

        public IActionResult DeleteArea(int id)
        {
            try
            {
                repoArea.Delete(id);
                int result = repoArea.Save();

                if (result > 0)
                {
                    // Return a JSON success response
                    return Json(new { success = true, message = "Member deleted successfully." });
                }
                else
                {
                    // Return a JSON error response
                    return Json(new { success = false, message = "Failed to delete member." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while deleting the member." });
            }
        }
        //delete phone

        public IActionResult DeletePhone(int id)
        {
            try
            {
                repophone.Delete(id);
                int result = repophone.Save();

                if (result > 0)
                {
                    // Return a JSON success response
                    return Json(new { success = true, message = "Member deleted successfully." });
                }
                else
                {
                    // Return a JSON error response
                    return Json(new { success = false, message = "Failed to delete member." });
                }
            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while deleting the member." });
            }
        }

        [HttpGet]
        //[Authorize(Roles = "Admin")] create sector
        public IActionResult CreatePhone(int id, int area_id)
        {
            if (id <= 0)
            {
                ViewBag.title = "اضافة تليفون";
                ViewBag.region_id = area_id;  

                return View();
            }
            else
            {
                ViewBag.title = "تعديل تليفون";
                ViewBag.region_id = area_id;
                PhonesSectors phone = repophone.GetByID(id);
                return View(phone);
            }

        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        [AutoValidateAntiforgeryToken]
        public IActionResult CreatePhone(PhonesSectors phone)
        {
            if (phone.Id == 0 && ModelState.IsValid)
            {
                repophone.Add(phone);
                repophone.Save();
                return RedirectToAction("IndexPhones");

            }
            else if (phone.Id > 0 && ModelState.IsValid)
            {
                PhonesSectors editpone = repophone.GetByID(phone.Id);
                if (editpone != null)
                {
                    editpone.PhoneNumber = phone.PhoneNumber;


                    repophone.Update(editpone);
                    repophone.Save();
                    return RedirectToAction("IndexPhones");

                }
            }
            return View(phone);
        }




    }
}