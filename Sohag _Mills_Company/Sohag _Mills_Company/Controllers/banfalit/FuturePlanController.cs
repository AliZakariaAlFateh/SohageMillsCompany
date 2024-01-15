using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.ViewModels;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
namespace Sohag__Mills_Company.Controllers
{
    public class FuturePlanController : Controller
    {


        IRepository<FuturePlan> repostory;
        IRepository<FuturePlan_Details> RepoDetails;
        private FuturePlan Plan { get; }
        private readonly ECcontext db;
        //private IProduct Product;
        public FuturePlanController(IRepository<FuturePlan> _repostory, IRepository<FuturePlan_Details> _RepoDetails, ECcontext _db)
        {

            repostory = _repostory;
            RepoDetails = _RepoDetails;
            db = _db;
        }


        public IActionResult IndexPlan()
        {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)repostory.GetAll().Count() / 5m);
            //IEnumerable<Shareholder_Structure> Data = repostory.GetAll();
            //ViewBag.Cat = repostory.GetAll();
            var Plan1 = repostory.GetAll();
            //"../DashBord/Index"
            return View(Plan1);

        }



        public IActionResult GetAllPlan(int pageNumber, int pageSize = 5)
        {
            var Plan1 = repostory.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/FuturePlan/Partial/_TeamsTables.cshtml", Plan1);
        }

        public IActionResult IndexPlanDetailsTableIndex()
        {
            //var Plan1 = repostory.GetAll();FuturePlans
            //var Plan_Details = db.FuturePlans.Include("FuturePlan_details").ToList();
            //theis code is true .....
            //var planDetails = db.FuturePlans.Include("PlanDetails").ToList();

            // Assuming you have a list of FuturePlan and FuturePlanDetail objects
            //this code is true ....
            //List<FuturePlanViewModel> viewModels = planDetails.Select(plan => new FuturePlanViewModel
            //{
            //    FuturePlan = plan,
            //    FuturePlanDetails = plan.PlanDetails.ToList()
            //}).ToList();

            //this code is true .....
            var planWithDetails = db.FuturePlans
                    .Include(c => c.PlanDetails)
                    .ToList();

            return View("../Shared/_IndexPlan_DetailsTableIndex", planWithDetails);





            //return View("../Shared/_IndexPlan_DetailsTableIndex", planWithDetails);


            //return View("../Shared/_IndexPlan_DetailsTableIndex", Plan_Details);

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

                return View("Create", Plan1 ?? new FuturePlan());
            }
        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[AutoValidateAntiforgeryToken]
        public IActionResult Create(FuturePlan NewPlan)
        {
            if (NewPlan.Id == null && ModelState.IsValid)
            {
                try
                {
                    repostory.Add(NewPlan);
                    repostory.Save();
                    //Index
                    return RedirectToAction("IndexPlan");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Plan is invalid " + ex.Message);
                }
            }
            else if (NewPlan.Id > 0 && ModelState.IsValid)
            {
                var oldObj = repostory.GetByID(NewPlan.Id);
                if (oldObj != null)
                {

                    oldObj.Plan_Name = NewPlan.Plan_Name;
                    oldObj.Plane_Year = NewPlan.Plane_Year;

                    repostory.Update(oldObj);
                    repostory.Save();
                    return RedirectToAction("IndexPlan");

                }
            }


            return View(NewPlan);
        }

        [HttpGet]
        public JsonResult Details(int id)
        {
            try
            {
                //var member = repostory.GetByID(id);
                var member = db.FuturePlans
                        .Where(p => p.Id == id)
                        .Select(p=>new FuturePlanDTO 
                        { Id=p.Id , Plan_Name=p.Plan_Name , 
                        Plane_Year=p.Plane_Year , 
                        PlanDetails=p.PlanDetails.Select(c=>new FuturePlanDetailsDTO { Id=c.Id, Plan_Text=c.Plan_Text }).ToList() })
                       .ToList();
                return Json(member);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = "Failed to Load Data." });

            }
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



        public IActionResult DeleteDetailsFuturePlan(int id)
        {
            try
            {
                RepoDetails.Delete(id);
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

    }
}