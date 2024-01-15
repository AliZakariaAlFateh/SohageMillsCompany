

using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using System.Linq;

namespace Sohag__Mills_Company.Services
{
    public class RegionRepo : Repository<Region_Sector_Details>, IRegionRepo
    {

        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;

        public RegionRepo(ECcontext _db, IWebHostEnvironment _webHost) : base(_db, _webHost)
        {
            this.db = _db;

            this.webHost = _webHost;
        }
        public override IEnumerable<Region_Sector_Details> GetAll()
        {
            return db.Set<Region_Sector_Details>().Include(s => s.Sector).ToList();
        }
        public override Region_Sector_Details GetByID(int? Id)
        {
            return db.Set<Region_Sector_Details>().Include(s => s.Sector).FirstOrDefault(s => s.Id == Id);
        }
    }
}
