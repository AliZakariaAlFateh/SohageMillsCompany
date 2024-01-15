

using Microsoft.EntityFrameworkCore;
using NuGet.Protocol.Core.Types;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using System.Linq;

namespace Sohag__Mills_Company.Services
{
    public class FuturePlan_DetailsRepo : Repository<FuturePlan> 
    {
        
        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;
        
        public FuturePlan_DetailsRepo(ECcontext _db, IWebHostEnvironment _webHost):base(_db,_webHost)
        {
            this.db = _db;
            
            this.webHost = _webHost;
        }
        //public  List<FuturePlan> GetAllBYIDforDetails(int id)
        //{
        //    var member = db.FuturePlans
        //            .Where(p => p.Id == id)
        //            .Include("PlanDetails")
        //            .ToList();
        //    return member;
        //    //var member = db.FuturePlans.Include("PlanDetails").Select(p => p.Id == id).ToList();

        //    //return db.Set<FuturePlan>().Include(s=>s.PlanDetails).ToList();

        //}

        public IEnumerable<FuturePlan> GetAllBYIDforDetails(int id)
        {

            var member = db.FuturePlans.Select(f => f.Id == id);
            //var member = db.FuturePlans
            //    .Where(p => p.Id == id)
            //    .Include("PlanDetails")
            //    .ToList();
            return (IEnumerable<FuturePlan>)member;


        }


    }
}
