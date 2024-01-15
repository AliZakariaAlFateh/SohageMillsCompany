using Sohag__Mills_Company.Models.Entity;

namespace Sohag__Mills_Company.Services
{
    
    public interface IRepository<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> GetAll();
        TEntity GetByID(int? Id);
        void Add(TEntity entity);
        void Delete(int Id);
        void Update(TEntity entity);
        int Save();
  
        public Region_Sector_Details LastId();
    }
}
