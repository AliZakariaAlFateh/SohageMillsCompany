

using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;

namespace Sohag__Mills_Company.Services
{
    public class PhonesRepo : Repository<PhonesSectors>, IPhonesRepo
    {

        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;

        public PhonesRepo(ECcontext _db, IWebHostEnvironment _webHost) : base(_db, _webHost)
        {
            this.db = _db;

            this.webHost = _webHost;
        }
        public override IEnumerable<PhonesSectors> GetAll()
        {
            return db.Set<PhonesSectors>().Include(s => s.Phones_Address_Details).ToList();
        }
        public  IEnumerable<Sector> GetAll_Sectors_With_Region_phones()
        {
            return db.Set<Sector>().Include(s => s.Region_Sector_Details).ThenInclude(a => a.PhonesSectors).ToList();
        }
        public override PhonesSectors GetByID(int? Id)
        {
            return db.Set<PhonesSectors>().Include(s => s.Phones_Address_Details).FirstOrDefault(s => s.Id == Id);
        }
    }
}
