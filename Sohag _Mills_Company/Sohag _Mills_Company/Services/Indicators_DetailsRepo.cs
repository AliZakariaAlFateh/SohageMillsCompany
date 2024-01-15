

using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using System.Linq;

namespace Sohag__Mills_Company.Services
{
    public class Indicators_DetailsRepo : Repository<Indicators_Details> 
    {
        
        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;

        public Indicators_DetailsRepo(ECcontext _db, IWebHostEnvironment _webHost):base(_db,_webHost)
        {
            this.db = _db;
            
            this.webHost = _webHost;
        }
        public override IEnumerable<Indicators_Details> GetAll()
        {
            return db.Set<Indicators_Details>().Include(s=>s.Statements).ToList();
        }
        public override Indicators_Details GetByID(int? Id)
        {
            return db.Set<Indicators_Details>().Include(s=>s.Statements).FirstOrDefault(s=>s.id==Id) ;
        }
        public  IEnumerable<Indicators_Details> GetAllForLast3Years()
        {
            return db.Set<Indicators_Details>().Include(s => s.Statements).Include(s=>s.Statements.Indicators).Where(i=>i.Year_Statement.Value.Year==DateTime.Now.Year||
            i.Year_Statement.Value.Year == (DateTime.Now.Year-1)|| (i.Year_Statement.Value.Year == DateTime.Now.Year+1)).ToList();
        }

        public IEnumerable<Indicators_Details> GetAllForLast3YearsForStamtment(Statement s)
        {
            return db.Set<Indicators_Details>().Where(i=>i.StatementId==s.Id).Include(s => s.Statements).Include(s => s.Statements.Indicators).Where(i => i.Year_Statement.Value.Year == DateTime.Now.Year ||
            i.Year_Statement.Value.Year == (DateTime.Now.Year - 1) || (i.Year_Statement.Value.Year == DateTime.Now.Year + 1)).ToList();
        }
    }
}
