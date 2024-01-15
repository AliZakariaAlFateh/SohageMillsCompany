using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using System.Diagnostics;

namespace Sohag__Mills_Company.Controllers
{
    public class AboutCompanyController : Controller
    {
        private readonly IRepository<About_Company> repo;

        public AboutCompanyController(IRepository<About_Company> _repo)
        {
            repo = _repo;
        }
        public IActionResult IndexAdminAbout()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repo.GetAll().Count() / 5m);

           return View(this.repo.GetAll());
            //return View();
        }
            public IActionResult GetAllAbout(int pageNumber, int pageSize = 5)
            {
                var Abouts = repo.GetAll()
               .OrderBy(p => p.Id)
               .Skip((pageNumber - 1) * pageSize)
               .Take(pageSize)
               .ToList();
                return PartialView("Views/AboutCompany/partialView/_AboutTables.cshtml", Abouts);
            }


        public IActionResult IndexAbout()
        {
            
            return View(repo.GetAll());
         
        }
        [HttpGet]
        public IActionResult CreateAbout(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "إضافة نبذة عن الشركة";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل نبذة عن الشركة";
                var aboutcompany = repo.GetByID(id);
                return View(aboutcompany);
            }
        }
        [HttpPost]
         [ValidateAntiForgeryToken]
        public IActionResult CreateAbout(About_Company aboutcomp)
        {
            if (aboutcomp.Id == 0 && ModelState.IsValid)
            {
                repo.Add(aboutcomp);
                repo.Save();
                return RedirectToAction("IndexAdminAbout");

            }
            else if (aboutcomp.Id > 0 && ModelState.IsValid)
            {
                var aboutcom = repo.GetByID(aboutcomp.Id);
                if (aboutcom != null)
                {
                    aboutcom.title = aboutcomp.title;
                    aboutcom.Description = aboutcomp.Description;
                  
                    repo.Update(aboutcom);
                    repo.Save();
                    return RedirectToAction("IndexAdminTeam");

                }
            }
                return View(aboutcomp);
        
        }
        [HttpGet]
        public JsonResult Details(int id)
        {
            var res = repo.GetByID(id);
            return Json(new { res });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                repo.Delete(id);
                int result = repo.Save();

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
        public JsonResult GetAllAboutcompany()
        {
            var res = repo.GetAll();
            return Json(new { res });
        }

    }
}