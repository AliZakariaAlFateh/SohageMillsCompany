using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using System.Diagnostics;
using System.Security.Claims;
namespace Sohag__Mills_Company.Controllers
{
    public class IndicatorsController : Controller
    {


        IRepository<Indicators> IndicatorsRepostory;
        private Indicators indicators { get; }

        
        public IndicatorsController(IRepository<Indicators> _indicatorsRepostory)
        {
            IndicatorsRepostory = _indicatorsRepostory ;
        }


        public IActionResult Index() {

            ViewBag.PageCount = (int)Math.Ceiling((decimal)IndicatorsRepostory.GetAll().Count() / 5m);
            return View(IndicatorsRepostory.GetAll());
        
        }
        public IActionResult GetIndicators(int pageNumber, int pageSize = 5)
        {
            var Indicators = IndicatorsRepostory.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("_Indicators", Indicators);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Indicators NewIndicators)
        {
            if (ModelState.IsValid)
            {
                if(NewIndicators.Type_Indicator==null)
                {
                    ModelState.AddModelError("", "الموشر مطلوب" );
                    return View(NewIndicators);
                }
                var Indicators = IndicatorsRepostory.GetAll();
                foreach (var item in Indicators)
                {
                    if (NewIndicators.Type_Indicator == item.Type_Indicator)
                    {
                        ModelState.AddModelError("", "هذا المؤشر موجود من قبل بنفس الاسم يرجى ادخال اسم مختلف");
                        return View(NewIndicators);
                    }
                }
                try
                {
                    IndicatorsRepostory.Add(NewIndicators);
                    IndicatorsRepostory.Save();
                    //Index
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Indicator is invalid " + ex.Message);
                }
            }
            return View(NewIndicators);
        }



        [HttpGet]
        
        public IActionResult Edit(int id)
        {
            return View(IndicatorsRepostory.GetByID(id));
        }


        [HttpPost]
        public IActionResult Edit(int id, Indicators NewIndicatorsForEdited)
        {
            //Who Is Logined Now

            Indicators IndicatorsForEdited = IndicatorsRepostory.GetByID(id);
            if (ModelState.IsValid)
            {
                var Indicators = IndicatorsRepostory.GetAll();
                foreach (var item in Indicators)
                {
                    if (NewIndicatorsForEdited.Type_Indicator == item.Type_Indicator && NewIndicatorsForEdited.Id == item.Id)
                    {
                        ModelState.AddModelError("", "هذا المؤشر موجود من قبل بنفس الاسم يرجى ادخال اسم مختلف");
                        return View(NewIndicatorsForEdited);
                    }
                }
                if (NewIndicatorsForEdited.Type_Indicator != null|| NewIndicatorsForEdited.Type_Indicator != "")
                {
                    IndicatorsForEdited.Type_Indicator = NewIndicatorsForEdited.Type_Indicator;
                }
                
                IndicatorsRepostory.Save();
                return RedirectToAction(nameof(Index));
                
            }

            ModelState.AddModelError("", "الموشر مطلوب");
            return View(IndicatorsRepostory.GetByID(id));

        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            IndicatorsRepostory.Delete(id);
            IndicatorsRepostory.Save();
            return RedirectToAction("Index");

        }

    }
}