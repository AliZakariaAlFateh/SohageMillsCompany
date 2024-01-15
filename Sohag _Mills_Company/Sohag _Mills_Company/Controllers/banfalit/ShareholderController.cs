using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
namespace Sohag__Mills_Company.Controllers
{
    public class ShareholderController : Controller
    {


        IRepository<Shareholder_Structure> repostory;

        private Shareholder_Structure SharHolders { get; }

        //private IProduct Product;
        public ShareholderController(IRepository<Shareholder_Structure> _repostory)
        {

            repostory = _repostory;

        }

        
        public IActionResult IndexSharHolders() {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)repostory.GetAll().Count() / 5m);
            //IEnumerable<Shareholder_Structure> Data = repostory.GetAll();
            //ViewBag.Cat = repostory.GetAll();
            var Shareholder_Structure = repostory.GetAll();
            //"../DashBord/Index"
            return View(Shareholder_Structure);
        
        }



        public IActionResult GetAllShareHolders(int pageNumber, int pageSize = 5)
        {
            var Teams = repostory.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("Views/Shareholder/Partial/_TeamsTables.cshtml", Teams);
        }

        public IActionResult IndexShareHoldersTableIndex()
        {
            var Shareholder_Structure = repostory.GetAll();
            return View("../Shared/_IndexShareHoldersTableIndex", Shareholder_Structure);

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
                return View();
            }
            else
            {
                var SharHolder = repostory.GetByID(id);

                return View("Create", SharHolder ?? new Shareholder_Structure());
            }
        }


        [HttpPost]
        //[Authorize(Roles = "Admin")]
        //[AutoValidateAntiforgeryToken]
        public IActionResult Create(Shareholder_Structure NewSharHolders)
        {
            if (NewSharHolders.Id == null && ModelState.IsValid)
            {
                try
                {
                    repostory.Add(NewSharHolders);
                    repostory.Save();
                    //Index
                    return RedirectToAction("IndexSharHolders");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "ShareHolder is invalid " + ex.Message);
                }
            }
            else if (NewSharHolders.Id > 0 && ModelState.IsValid)
            {
                var oldObj = repostory.GetByID(NewSharHolders.Id);
                if (oldObj != null)
                {

                    oldObj.Statement = NewSharHolders.Statement;
                    oldObj.Number_Shares = NewSharHolders.Number_Shares;
                    oldObj.Value_Bound = NewSharHolders.Value_Bound;
                    oldObj.Percentage = NewSharHolders.Percentage;

                    repostory.Update(oldObj);
                    repostory.Save();
                    return RedirectToAction("IndexSharHolders");

                }
            }


            return View(NewSharHolders);
        }


        [HttpGet]
        public JsonResult Details(int id)
        {
            var member = repostory.GetByID(id);
            return Json(new { member });
        }


        //[HttpGet]
        ////[Authorize(Roles = "Admin")]
        //public IActionResult Edit(int id)
        //{
        //    return View(repostory.GetByID(id));
        //}


        //[HttpPost]
        ////[AutoValidateAntiforgeryToken]
        ////[Authorize(Roles = "Admin")]
        //public IActionResult Edit(int id, Shareholder_Structure NewSharHoldersForEdited)
        //{
        //    //Who Is Logined Now

        //    Shareholder_Structure ShareholderEdited = repostory.GetByID(id);
        //    if (ModelState.IsValid)
        //    {
        //        string ImageName = "";
        //        //var Image_prod=repostory.GetByID(id).Image;
        //        //select Who Logined Now
        //            //var userID = ProductEdited.UserID;
        //            if (ShareholderEdited != null)
        //            {
        //                ShareholderEdited.Statement = NewSharHoldersForEdited.Statement;
        //                ShareholderEdited.Percentage = NewSharHoldersForEdited.Percentage;
        //                ShareholderEdited.Value_Bound = NewSharHoldersForEdited.Value_Bound;
        //                ShareholderEdited.Number_Shares = NewSharHoldersForEdited.Number_Shares;
        //            }
        //            //repostory.Update(ProductEdited);
        //            repostory.Save();
        //            return RedirectToAction(nameof(Index));
                
        //    }
            

        //    ModelState.AddModelError("", "Erooooooooooooooooooooooorrrrrrrrrrrrrrr");
           
        //    return View(repostory.GetByID(id));

        //}





        //[Authorize(Roles = "Admin")]
        //public IActionResult Delete(int id)
        //{
        //    repostory.Delete(id);
        //    //repostory.Save();
        //    return RedirectToAction(nameof(Index));

        //}


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





    }
}