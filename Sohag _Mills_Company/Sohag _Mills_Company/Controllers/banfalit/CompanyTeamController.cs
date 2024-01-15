using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.Services.IServices;
using Sohag__Mills_Company.Services.map;
using System.Diagnostics;

namespace Sohag__Mills_Company.Controllers
{
    public class CompanyTeamController : Controller
    {
        private readonly IRepository<CampanyTeam> repo;
        private readonly IFilePdf fileRepo;
        private MapObject mapObject;


        public CompanyTeamController(IRepository<CampanyTeam> _repo,IFilePdf _fileRepo)
        {
            repo = _repo;
            fileRepo = _fileRepo;
            mapObject = new MapObject();
        }
        public IActionResult IndexAdminTeam()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repo.GetAll().Count() / 5m);

            return View(this.repo.GetAll());
            //return View();
        }
        public IActionResult GetAllTeams(int pageNumber, int pageSize = 5)
        {
            var Teams = repo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/CompanyTeam/partialView/_TeamsTables.cshtml", Teams);
        }


        public IActionResult IndexTeam()
        {
            var CampnyTeams = repo.GetAll();
            return View(CampnyTeams);

        }
        [HttpGet]
        public IActionResult CreateTeam(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "إضافة بيانات العضو";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل فى بيانات العضو";
                var companyTeam = repo.GetByID(id);
                
                return View("createTeam", companyTeam ?? new CampanyTeam());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateTeam(CampanyTeam campanyTeam)
        {
            string? ImageName = null;
            if (campanyTeam.ImageIFormFile != null)
                ImageName = fileRepo.UploadImage(campanyTeam.ImageIFormFile);

            if (campanyTeam.Id == 0 && ModelState.IsValid)
            {
                campanyTeam.ImageName = ImageName;
                repo.Add(campanyTeam);
                repo.Save();
                return RedirectToAction("IndexAdminTeam");

            }
            else if (campanyTeam.Id > 0 && ModelState.IsValid)
            {
                var oldteam = repo.GetByID(campanyTeam.Id);
                if (oldteam != null)
                {
                    if (ImageName != null)
                    {
                        //oldteam.ImageName = ImageName;
                        campanyTeam.ImageName = ImageName;
                    }
                    oldteam = mapObject.MapProperties(campanyTeam, oldteam);
                  /*  oldteam.Name = campanyTeam.Name;
                    oldteam.Postion = campanyTeam.Postion;
                    oldteam.postionId = campanyTeam.postionId;
                    oldteam.Nationaity = campanyTeam.Nationaity;
                    oldteam.Representation_Body = campanyTeam.Representation_Body;
                    oldteam.nickname = campanyTeam.nickname*/;
                    repo.Update(oldteam);
                    repo.Save();
                    return RedirectToAction("IndexAdminTeam");

                }
            }

            return View(campanyTeam);


        }
        [HttpGet]
        public JsonResult Details(int id)
        {
            var member = repo.GetByID(id);
            return Json(new { member });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var member = repo.GetByID(id);
                member.IsDeleted = true;
                //repo.Delete(id);
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
        public JsonResult GetAllMemberTeams()
        {
            var members = repo.GetAll();
            return Json(new { members });
        }

    }
}