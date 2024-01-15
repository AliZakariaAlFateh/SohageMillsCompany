using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.Entity;
using Sohag__Mills_Company.Services;
using System.Diagnostics;

namespace Sohag__Mills_Company.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly StatementRepo StatementRepo;
        Indicators_DetailsRepo Indicators_DetailsRepo;
        IRepository<Indicators> IndicatorsRepostory ;

        private readonly IRepository<CampanyTeam> repo;
        private readonly IRepository<ImagesCarousel> RepoImages;
        private readonly IRepository<IsoCertification> RepoCertification;
        private readonly IRepository<IdentityUser> repoUsers;


        public HomeController(ILogger<HomeController> logger, StatementRepo _StatementRepo, Indicators_DetailsRepo indicators_DetailsRepo,
           IRepository<Indicators> _IndicatorsRepostory, IRepository<CampanyTeam> repo, IRepository<ImagesCarousel> RepoImages, IRepository<IsoCertification> repoCertification, IRepository<IdentityUser> repoUsers)
        {
            _logger = logger;
            StatementRepo = _StatementRepo;
            Indicators_DetailsRepo = indicators_DetailsRepo;
            IndicatorsRepostory = _IndicatorsRepostory;
            this.repo = repo;
            this.RepoImages = RepoImages;
            this.RepoCertification = repoCertification;
            this.repoUsers = repoUsers;
        }
        public IActionResult Index()
        {

            //ViewBag.AboutCompany = repoAboutcompany.GetAll();
            ViewBag.Images = RepoImages.GetAll();
            ViewBag.Certifications=RepoCertification.GetAll();
            //ViewBag.Users= repoUsers.GetAll();
            var CampnyTeams = repo.GetAll();
            return View(CampnyTeams);
        }
        public IActionResult Login()
        {
            return View();
        }
        public IActionResult Indicators_Details()
        {

            
            //ViewBag.count = IndicatorsRepostory.GetAll().Count() == 0 ? 0 : IndicatorsRepostory.GetAll().Count();
            ViewBag.IndicatorId = IndicatorsRepostory.GetAll().Select(a=>a.Id).ToList();
            
            var statments = StatementRepo.GetAllbyDetallis();
            foreach (var item in statments)
            {
                item.indicators_Details = Indicators_DetailsRepo.GetAllForLast3YearsForStamtment(item).ToList();
                
            }
            return View(statments);
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}