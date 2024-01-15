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
    public class ImageCarouselController : Controller
    {
        private readonly IRepository<ImagesCarousel> repo;
        private readonly IFilePdf fileRepo;
        private MapObject mapObject;


        public ImageCarouselController(IRepository<ImagesCarousel> _repo,IFilePdf _fileRepo)
        {
            repo = _repo;
            fileRepo = _fileRepo;
            mapObject = new MapObject();
        }
        public IActionResult IndexImages()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repo.GetAll().Count() / 5m);

            return View(this.repo.GetAll());
            //return View();
        }
        public IActionResult GetAllImages(int pageNumber, int pageSize = 5)
        {
            var Teams = repo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/ImageCarousel/partialView/_TeamsTables.cshtml", Teams);
        }


        public IActionResult IndexTeam()
        {
            var CampnyTeams = repo.GetAll();
            return View(CampnyTeams);

        }
        [HttpGet]
        public IActionResult CreateImage(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "إضافة بيانات صورة";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل فى بيانات صورة";
                var ImageCarousel_obj = repo.GetByID(id);
                
                return View("CreateImage", ImageCarousel_obj ?? new ImagesCarousel());
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateImage(ImagesCarousel Image_New)
        {
            string? ImageName = null;
            if (Image_New.ImageIFormFile != null)
                ImageName = fileRepo.UploadImage(Image_New.ImageIFormFile);

            if (Image_New.Id == 0 && ModelState.IsValid)
            {
                Image_New.ImageName = ImageName;
                repo.Add(Image_New);
                repo.Save();
                return RedirectToAction("IndexImages");

            }
            else if (Image_New.Id > 0 && ModelState.IsValid)
            {
                var OldImage = repo.GetByID(Image_New.Id);
                if (OldImage != null)
                {
                    if (ImageName != null)
                    {
                        //oldteam.ImageName = ImageName;
                        Image_New.ImageName = ImageName;
                    }
                    OldImage = mapObject.MapProperties(Image_New, OldImage);
                  /*  oldteam.Name = campanyTeam.Name;
                    oldteam.Postion = campanyTeam.Postion;
                    oldteam.postionId = campanyTeam.postionId;
                    oldteam.Nationaity = campanyTeam.Nationaity;
                    oldteam.Representation_Body = campanyTeam.Representation_Body;
                    oldteam.nickname = campanyTeam.nickname*/;
                    repo.Update(OldImage);
                    repo.Save();
                    return RedirectToAction("IndexImages");

                }
            }

            return View(Image_New);


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