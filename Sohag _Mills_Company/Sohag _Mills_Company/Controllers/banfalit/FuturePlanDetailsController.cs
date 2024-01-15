using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
namespace Sohag__Mills_Company.Controllers
{
    public class FuturePlanDetailsController : Controller
    {


        IRepository<FuturePlan_Details> repostory;

        private FuturePlan Plan { get; }

        private readonly ECcontext db;
        //private IProduct Product;
        public FuturePlanDetailsController(IRepository<FuturePlan_Details> _repostory, ECcontext _db)
        {

            repostory = _repostory;
            db = _db;
        }


        public IActionResult IndexPlanDetails()
        {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)repostory.GetAll().Count() / 5m);
            //IEnumerable<Shareholder_Structure> Data = repostory.GetAll();
            //ViewBag.Cat = repostory.GetAll();
            var Plan1 = repostory.GetAll();
            //"../DashBord/Index"
            return View(Plan1);

        }



        public IActionResult GetAllPlanDetails(int pageNumber, int pageSize = 5)
        {
            var Plan1 = repostory.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/FuturePlanDetails/Partial/_TeamsTables.cshtml", Plan1);
        }

        public IActionResult IndexPlanDetailsTableIndex()
        {
            var Plan1 = repostory.GetAll();
            return View("../Shared/_IndexPlanTableIndex", Plan1);

        }



        [HttpGet]
        public JsonResult GetAllForShow()
        {
            var member = repostory.GetAll();
            return Json(new { member });
        }


        [HttpGet]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create(int id)
        {
            if (id <= 0)
            {
                ViewBag.title = "إضافة خطة  ";
                return View();
            }
            else
            {
                ViewBag.title = "تعديل خطة  ";
                var Plan1 = repostory.GetByID(id);

                return View("Create", Plan1 ?? new FuturePlan_Details());
            }
        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[AutoValidateAntiforgeryToken]
        public IActionResult Create(FuturePlan_Details NewPlanDetails)
        {
            if (NewPlanDetails.Id == null && ModelState.IsValid)
            {
                try
                {
                    repostory.Add(NewPlanDetails);
                    repostory.Save();
                    //Index
                    return RedirectToAction("IndexPlan");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Plan is invalid " + ex.Message);
                }
            }
            else if (NewPlanDetails.Id > 0 && ModelState.IsValid)
            {
                var oldObj = repostory.GetByID(NewPlanDetails.Id);
                if (oldObj != null)
                {
                    

                    oldObj.Plan_Text = NewPlanDetails.Plan_Text;
                    oldObj.FuturePlanId = NewPlanDetails.FuturePlanId;

                    repostory.Update(oldObj);
                    repostory.Save();
                    return RedirectToAction("IndexPlan");

                }
            }


            return View(NewPlanDetails);
        }


        [HttpGet]
        public JsonResult Details(int id)
        {
            var member = repostory.GetByID(id);
            return Json(new { member });
        }

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



        [HttpPost]
        //For Create Course 
        public ActionResult CreateFutureDetails(FuturePlan_Details PlanDetails)
        {
            
            try
            {

                db.FuturePlan_details.Add(PlanDetails);
                db.SaveChanges();
                return Json(new { success = false, message = "Data Saved Successfullyyyyyyyyyyyyy." });

            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while inserting data." });
            }

        }


        public ActionResult Edit(FuturePlan_Details PlanDetails)
        {

            try
            {
                var NewDetails = db.FuturePlan_details.Where(FTureDetails => FTureDetails.Id == PlanDetails.Id).FirstOrDefault();
                //string uniqueFileName = UploadFile(Dept.ImageFile);
                //dept.FileForImage = uniqueFileName;

                NewDetails.Plan_Text = PlanDetails.Plan_Text;
                NewDetails.FuturePlanId = PlanDetails.FuturePlanId;
                db.SaveChanges();
                //db.FuturePlan_details.Add(PlanDetails);
                //db.SaveChanges();
                return Json(new { success = false, message = "Data Saved Successfullyyyyyyyyyyyyy." });

            }
            catch (Exception ex)
            {
                // Log the exception
                return Json(new { success = false, message = "An error occurred while inserting data." });
            }

        }




    }
}