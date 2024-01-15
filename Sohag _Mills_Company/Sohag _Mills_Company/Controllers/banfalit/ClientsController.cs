using Microsoft.AspNetCore.Mvc;
using NuGet.Protocol.Core.Types;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using Sohag__Mills_Company.Services.map;
using System.Diagnostics;

namespace Sohag__Mills_Company.Controllers
{
    public class ClientsController : Controller
    {
        private readonly IRepository<clients> repo;
        private readonly MapObject map;

        public ClientsController(IRepository<clients> _repo, MapObject _map)
        {
            repo = _repo;
            map =_map;
        }
        
        public IActionResult GetClients(int pageNumber, int pageSize = 5)
        {
            var clients = repo.GetAll()
            .OrderBy(p => p.Id)
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToList();
            return PartialView("_Clients", clients);
        }
        public IActionResult Index()
        {
            ViewBag.PageCount = (int)Math.Ceiling((decimal)repo.GetAll().Count() / 5m);
            return View(repo.GetAll());
         
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(clients Newclients)
        {
            if (ModelState.IsValid)
            {
                if (Newclients.Clint_Code == null)
                {
                    ModelState.AddModelError("", "كود العميل مطلوب");
                    return View(Newclients);
                }
                try
                {
                    repo.Add(Newclients);
                    repo.Save();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "clint is invalid " + ex.Message);
                }
            }
            return View(Newclients);
        }
        public IActionResult Edit(int id)
        {
            return View(repo.GetByID(id));
        }


        [HttpPost]
        public IActionResult Edit(int id, clients NewClientsForEdited)
        {
            //Who Is Logined Now

            clients ClientForEdited = repo.GetByID(id);
            if (ModelState.IsValid)
            {
                
                if (ClientForEdited != null)
                {
                    ClientForEdited = map.MapProperties(NewClientsForEdited, ClientForEdited);
                }

                repo.Save();
                return RedirectToAction(nameof(Index));

            }

            ModelState.AddModelError("", "Opeeees");

            return View(repo.GetByID(id));

        }

        //[Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            repo.Delete(id);
            repo.Save();
            return RedirectToAction("Index");

        }

    }
}