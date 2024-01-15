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
    public class StatementController : Controller
    {


        IRepository<Statement> StatementRepostory;
        IRepository<Indicators> IndicatorsRepostory;
        StatementRepo StatementRepo;




        //private IProduct Product;
        public StatementController(IRepository<Statement> _StatementRepostory, IRepository<Indicators> _indicatorsRepostory,
            StatementRepo _statementRepo)
        {
            StatementRepostory = _StatementRepostory;
            IndicatorsRepostory = _indicatorsRepostory;
            StatementRepo = _statementRepo;
        }


        public IActionResult Index() {
            var s = StatementRepo.GetAll();

            ViewBag.PageCount = (int)Math.Ceiling((decimal)StatementRepostory.GetAll().Count() / 5m);
            return View(StatementRepostory.GetAll());
        
        }
        public IActionResult GetStatement(int pageNumber, int pageSize = 5)
        {
            var Statement = StatementRepo.GetAll()
           .OrderBy(p => p.Id)
           .Skip((pageNumber - 1) * pageSize)
           .Take(pageSize)
           .ToList();
            return PartialView("_Statement", Statement);
        }

        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            return View();
        }
        [HttpPost]
        public IActionResult Create(Statement NewStatement)
        {
            if (ModelState.IsValid)
            {
                if(NewStatement.Statement_Name==null)
                {
                    ModelState.AddModelError("", "البيان مطلوب" );
                    ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                    return View(NewStatement);
                }
                if (NewStatement.IndicatorId == null)
                {
                    ModelState.AddModelError("", "يجب اختيار مؤشر");
                    ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                    return View(NewStatement);
                }
                var Statements = StatementRepo.GetAll();
                foreach (var item in Statements)
                {
                    if (NewStatement.Statement_Name == item.Statement_Name && NewStatement.IndicatorId== item.IndicatorId)
                    {
                        ModelState.AddModelError("", "هذا البيان موجود من قبل بنفس المؤشر يرجى ادخال اسم مختلف");
                        ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                        return View(NewStatement);
                    }
                }
                
                try
                {
                    StatementRepostory.Add(NewStatement);
                    StatementRepostory.Save();
                    return RedirectToAction("index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Indicator is invalid"+ex.Message);
                }
            }
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            return View(NewStatement);
        }



        [HttpGet]
        public IActionResult Edit(int id)
        {
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(),"Id","Type_Indicator");
            return View(StatementRepostory.GetByID(id));
        }


        [HttpPost]
        public IActionResult Edit(int id, Statement NewStatementForEdited)
        {
            Statement StatementForEdited = StatementRepostory.GetByID(id);
            if (ModelState.IsValid)
            {
                var Statements = StatementRepo.GetAll();
                foreach (var item in Statements)
                {
                    if (NewStatementForEdited.Statement_Name == item.Statement_Name && NewStatementForEdited.IndicatorId == item.IndicatorId &&
                        NewStatementForEdited.Id != item.Id)
                    {
                        ModelState.AddModelError("", "هذا البيان موجود من قبل بنفس المؤشر يرجى ادخال اسم مختلف");
                        ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
                        return View(NewStatementForEdited);
                    }
                }
                if (NewStatementForEdited.Statement_Name != null || NewStatementForEdited.Statement_Name!= "")
                {
                    StatementForEdited.Statement_Name = NewStatementForEdited.Statement_Name;
                    StatementForEdited.IndicatorId = NewStatementForEdited.IndicatorId;
                }
                StatementRepostory.Save();
                return RedirectToAction(nameof(Index));
            }
            ModelState.AddModelError("", "البيان مطلوب");
            ViewBag.IndicatorId = new SelectList(IndicatorsRepostory.GetAll(), "Id", "Type_Indicator");
            return View(StatementRepostory.GetByID(id));

        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            StatementRepostory.Delete(id);
            StatementRepostory.Save();
            return RedirectToAction("Index");

        }

    }
}