

using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using System.Linq;

namespace Sohag__Mills_Company.Services
{
    public class StatementRepo : Repository<Statement> 
    {
        
        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;

        public StatementRepo(ECcontext _db, IWebHostEnvironment _webHost):base(_db,_webHost)
        {
            this.db = _db;
            
            this.webHost = _webHost;
        }
        public override IEnumerable<Statement> GetAll()
        {
            return db.Set<Statement>().Include(s=>s.Indicators).ToList();
        }
        public IEnumerable<Statement> GetAllbyDetallis()
        {
            return db.Set<Statement>().Where(s=>s.indicators_Details.Count()!=0).Include(s => s.Indicators)
                .Include( s => s.indicators_Details).ToList();
        }

        
    }
}
