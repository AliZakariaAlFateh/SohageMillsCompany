using Microsoft.AspNetCore.Mvc;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.Services.IServices;
using Sohag__Mills_Company.Services.map;

namespace Sohag__Mills_Company.Controllers.banfalit
{
    public class UploadFileController : Controller
    {
        private readonly IRepository<FilePdf> fileRepo;
        private readonly IFilePdf filePdf;
        private readonly MapObject mapObject;

        public UploadFileController(IRepository<FilePdf> fileRepo, IFilePdf filePdf, MapObject mapObject)
        {
            this.fileRepo = fileRepo;
            this.filePdf = filePdf;
            this.mapObject = mapObject;
        }

        public IActionResult IndexFiles()
        {
            return View(fileRepo.GetAll());
        }
        public IActionResult GetAllFiles_Base_type(int type_pdf)
        {
            return View(filePdf.GetAllFiles_Base_type(type_pdf));
        }
        public IActionResult IndexFilesAdmin()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)fileRepo.GetAll().Count() / 5m);

            return View(fileRepo.GetAll());
        }
        public IActionResult GetAllFiles(int pageNumber, int pageSize = 5)
        {
            var AllFiles = fileRepo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/UploadFile/partialView/_FilesTables.cshtml", AllFiles);
        }

        [HttpGet]
        public JsonResult GetAllFilesdropdown()
        {
            var res = fileRepo.GetAll().Select(d=>d.FileDescription);
            return Json(new { res });
        }

        [HttpGet]
        public IActionResult CreateFile(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "إضافة ملف";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل ملف";
                var file = fileRepo.GetByID(id);
                return View(file);
            }
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateFile(FilePdf file)
        {

            if (file.Id == 0 && ModelState.IsValid)
            {
                if (file.PdfIFormFile != null)
                {
                    file.PdfName = filePdf.UploadFile(file.PdfIFormFile, null);
                    //file.PdfName=filePdf.UploadImage(file.PdfIFormFile);

                }
                if (file.ImageIFormFile != null)
                {
                    file.ImageName = filePdf.UploadFile(file.ImageIFormFile, null);
                }
                fileRepo.Add(file);
                fileRepo.Save();
                return RedirectToAction("IndexFilesAdmin");

            }
            else if (file.Id > 0 && ModelState.IsValid)
            {
                var fileEdit = fileRepo.GetByID(file.Id);
                if (fileEdit != null)
                {
                    if (file.ImageIFormFile != null)
                    {
                        file.ImageName = filePdf.UploadFile(file.ImageIFormFile, fileEdit.ImageName);
                        //file.ImageName = filePdf.UploadImage(file.ImageIFormFile);
                    }
                    else
                    {
                        file.ImageName = fileEdit.ImageName;

                    }
                    if (file.PdfIFormFile != null)
                    {
                        file.PdfName = filePdf.UploadFile(file.PdfIFormFile, fileEdit.PdfName);
                        //file.PdfName = filePdf.UploadImage(file.PdfIFormFile);
                    }
                    else
                    {
                        file.PdfName= fileEdit.PdfName;
                    }
                    fileEdit = mapObject.MapProperties(file, fileEdit);
                    fileRepo.Update(fileEdit);
                    fileRepo.Save();
                    return RedirectToAction("IndexFilesAdmin");

                }
            }
            return View(file);

        }

        [HttpGet]
        public JsonResult Details(int id)
        {
            var res = fileRepo.GetByID(id);
            return Json(new { res });
        }
        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var fileDeleted = fileRepo.GetByID(id);
                fileDeleted.IsDeleted = true;

                /*  fileRepo.Delete(id);  */
                int result = fileRepo.Save();

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

    }
}
