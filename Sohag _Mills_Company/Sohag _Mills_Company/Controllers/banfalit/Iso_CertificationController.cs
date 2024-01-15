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
    public class Iso_CertificationController : Controller
    {
        private readonly IRepository<IsoCertification> repo;
        private readonly IFilePdf fileRepo;
        private MapObject mapObject;


        public Iso_CertificationController(IRepository<IsoCertification> _repo,IFilePdf _fileRepo)
        {
            repo = _repo;
            fileRepo = _fileRepo;
            mapObject = new MapObject();
        }
        public IActionResult IndexCertification()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repo.GetAll().Count() / 5m);

            return View(this.repo.GetAll());
            //return View();
        }
        public IActionResult GetAllIsoCertificate(int pageNumber, int pageSize = 5)
        {
            var Teams = repo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/Iso_Certification/partialView/_TeamsTables.cshtml", Teams);
        }


        public IActionResult IndexTeam()
        {
            var CampnyTeams = repo.GetAll();
            return View(CampnyTeams);

        }
        [HttpGet]
        public IActionResult CreateCertification(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "إضافة بيانات شهادة أيزو";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل فى بيانات شهادة أيزو";
                var IsoCertification_obj = repo.GetByID(id);
                
                return View("CreateCertification", IsoCertification_obj ?? new IsoCertification());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateCertification(IsoCertification IsoCertification_New)
        {
            string? ImageName = null;
            if (IsoCertification_New.ImageIFormFile != null)
                ImageName = fileRepo.UploadImage(IsoCertification_New.ImageIFormFile);

            if (IsoCertification_New.Id == 0 && ModelState.IsValid)
            {
                IsoCertification_New.ImageName = ImageName;
                repo.Add(IsoCertification_New);
                repo.Save();
                return RedirectToAction("IndexCertification");

            }
            else if (IsoCertification_New.Id > 0 && ModelState.IsValid)
            {
                var OldIsoCertification = repo.GetByID(IsoCertification_New.Id);
                if (OldIsoCertification != null)
                {
                    if (ImageName != null)
                    {
                        //oldteam.ImageName = ImageName;
                        IsoCertification_New.ImageName = ImageName;
                    }
                    OldIsoCertification = mapObject.MapProperties(IsoCertification_New, OldIsoCertification);
                  /*  oldteam.Name = campanyTeam.Name;
                    oldteam.Postion = campanyTeam.Postion;
                    oldteam.postionId = campanyTeam.postionId;
                    oldteam.Nationaity = campanyTeam.Nationaity;
                    oldteam.Representation_Body = campanyTeam.Representation_Body;
                    oldteam.nickname = campanyTeam.nickname*/;
                    repo.Update(OldIsoCertification);
                    repo.Save();
                    return RedirectToAction("IndexCertification");

                }
            }

            return View(IsoCertification_New);


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