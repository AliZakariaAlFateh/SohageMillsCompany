

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.ViewModels;
using System.Collections.Generic;
using System.Diagnostics;
using System.Security.Claims;
namespace Sohag__Mills_Company.Controllers
{
    public class Indicators_DetailsController : Controller
    {
        IRepository<Indicators> IndicatorsRepostory;
        IRepository<Statement> StatementRepostory;
        IRepository<Indicators_Details> Indicators_DetailsRepostory;
        Indicators_DetailsRepo Indicators_DetailsRepo;
        //StatementRepo StatementRepo;

        public Indicators_DetailsController(IRepository<Indicators> _IndicatorsRepostory,IRepository<Indicators_Details> _Indicators_DetailsRepostory,
            IRepository<Statement> _StatementRepostory, Indicators_DetailsRepo indicators_DetailsRepo)
        {
            IndicatorsRepostory = _IndicatorsRepostory;
            Indicators_DetailsRepostory = _Indicators_DetailsRepostory;
            StatementRepostory = _StatementRepostory;
            Indicators_DetailsRepo = indicators_DetailsRepo;
        }
        public IActionResult Index() {
            
            ViewBag.PageCount = (int)Math.Ceiling((decimal)Indicators_DetailsRepostory.GetAll().Count() / 5m);
            return View(Indicators_DetailsRepostory.GetAll());
        
        }
        public IActionResult GetIndicators_Details(int pageNumber, int pageSize = 5)
        {
            List<IndIndicators_DetailsVM> list = new List<IndIndicators_DetailsVM>();
            foreach(var item  in Indicators_DetailsRepo.GetAll())
            {
                IndIndicators_DetailsVM indIndicators_DetailsVM = new IndIndicators_DetailsVM()
                {
                    id = item.id,
                    Year_Statement = item.Year_Statement.Value.Year,
                    Quentity_Year = item.Quentity_Year,
                    statement_Name = StatementRepostory.GetByID(item.StatementId).Statement_Name,
                    Type_Indicator = IndicatorsRepostory.GetByID(item.Statements.IndicatorId).Type_Indicator
                };
                if (StatementRepostory.GetByID(item.StatementId) != null)
                {
                    var element = StatementRepostory.GetByID(item.StatementId);
                    indIndicators_DetailsVM.statement_Name = element.Statement_Name;
                }
                else
                    indIndicators_DetailsVM.statement_Name = "";

                if (IndicatorsRepostory.GetByID(item.Statements.IndicatorId) != null)
                    indIndicators_DetailsVM.Type_Indicator = IndicatorsRepostory.GetByID(item.Statements.IndicatorId).Type_Indicator;
                else
                    indIndicators_DetailsVM.Type_Indicator = "";
                
                list.Add(indIndicators_DetailsVM);

            }
            var Indicators_Details = Indicators_DetailsRepostory.GetAll()
           .OrderBy(p => p.id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("_Indicators_Details", list);
        }

        [HttpGet]
        public IActionResult Create(int id = 1 )
        {
            var Statment = StatementRepostory.GetAll().Where(s => s.IndicatorId == id);
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            return View();
        }
        public IActionResult JsonData(int id = 1)
        {
            var Statment = StatementRepostory.GetAll().Where(s => s.IndicatorId == id);

            if (Statment == null)
                Statment = StatementRepostory.GetAll();

            return Json(Statment);
        }
        [HttpPost]
        public IActionResult Create(Indicators_Details NewIndicators_Details)
        {
            if (ModelState.IsValid)
            {
                if (NewIndicators_Details.Quentity_Year == null)
                {
                    ModelState.AddModelError("", "كمية المؤشر مطلوبه");
                    ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                    return View(NewIndicators_Details);
                }
                if (NewIndicators_Details.Year_Statement.Value.Year < 2021)
                {
                    ModelState.AddModelError("", "السنه اكبر من ٢٠٢١");
                    ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                    return View(NewIndicators_Details);
                }
                if (NewIndicators_Details.StatementId == null )
                {
                    ModelState.AddModelError("", "يجب اختيار بيان مؤشر");
                    ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                    return View(NewIndicators_Details);
                }
                var Indicators_Details = Indicators_DetailsRepo.GetAll();
                foreach (var item in Indicators_Details)
                {
                    if(item.StatementId== NewIndicators_Details.StatementId && item.Statements.IndicatorId==NewIndicators_Details.Statements.IndicatorId &&
                        item.Year_Statement.Value.Year == NewIndicators_Details.Year_Statement.Value.Year)
                    {
                        ViewBag.Exist = "هذا العنصر موجود من قبل بنفس البيانات الرجاء ادخال بياتات مختلفة او تعديل العنصر";
                        ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                        return View(NewIndicators_Details);
                    }
                }
                try
                {
                    NewIndicators_Details.Statements = null;
                    Indicators_DetailsRepostory.Add(NewIndicators_Details);
                    Indicators_DetailsRepostory.Save();
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Indicators_Details is invalid" + ex.Message);
                }
            }
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            

            return View(NewIndicators_Details);
        }
        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            ViewBag.StatementId = new SelectList(StatementRepostory.GetAll(), "Id", "Statement_Name");
            return View(Indicators_DetailsRepo.GetByID(id));
        }


        [HttpPost]
        public IActionResult Edit(int id, Indicators_Details NewIndicators_DetailsForEdited)
        {
            Indicators_Details Indicators_DetailsForEdited = Indicators_DetailsRepostory.GetByID(id);
            if (ModelState.IsValid)
            {
                var Indicators_Details = Indicators_DetailsRepo.GetAll();
                foreach (var item in Indicators_Details)
                {
                    if (item.StatementId == NewIndicators_DetailsForEdited.StatementId && item.Statements.IndicatorId == NewIndicators_DetailsForEdited.Statements.IndicatorId &&
                        item.Year_Statement.Value.Year == NewIndicators_DetailsForEdited.Year_Statement.Value.Year && NewIndicators_DetailsForEdited.id != item.id)
                    {
                        ViewBag.Exist = "هذا العنصر موجود من قبل بنفس البيانات الرجاء ادخال بياتات مختلفة او تعديل العنصر";
                        ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                        return View(NewIndicators_DetailsForEdited);
                    }
                }
                if (NewIndicators_DetailsForEdited.Year_Statement != null && NewIndicators_DetailsForEdited.Quentity_Year!= null)
                {
                    Indicators_DetailsForEdited.StatementId = NewIndicators_DetailsForEdited.StatementId;
                    Indicators_DetailsForEdited.Year_Statement = NewIndicators_DetailsForEdited.Year_Statement;
                    Indicators_DetailsForEdited.Quentity_Year = NewIndicators_DetailsForEdited.Quentity_Year;
                    Indicators_DetailsForEdited.Statements = null;
                }
                Indicators_DetailsRepostory.Save();
                return RedirectToAction(nameof(Index));
            }
            
            ModelState.AddModelError("", "يرجى ادخال السنه والكمية");
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            return View(Indicators_DetailsRepo.GetByID(id));

        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            Indicators_DetailsRepostory.Delete(id);
            Indicators_DetailsRepostory.Save();
            return RedirectToAction("Index");

        }

    }
}