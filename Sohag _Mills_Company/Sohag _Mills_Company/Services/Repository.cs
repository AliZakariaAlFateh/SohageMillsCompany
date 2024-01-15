

using Microsoft.EntityFrameworkCore;
using Sohag__Mills_Company.Models;
using Sohag__Mills_Company.Models.DataContext;
using Sohag__Mills_Company.Models.Entity;
using System.Linq;

namespace Sohag__Mills_Company.Services
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        
        private readonly ECcontext db;
        private readonly IWebHostEnvironment webHost;

        public Repository(ECcontext _db, IWebHostEnvironment _webHost)
        {
            this.db = _db;
            webHost = _webHost;
            this.webHost = _webHost;
        }
        public virtual IEnumerable<TEntity> GetAll()
        {
            return db.Set<TEntity>().ToList();
        }

        public virtual TEntity GetByID(int? Id)
        {
            return db.Set<TEntity>().Find(Id);
        }
        
        public void Add(TEntity entity)
        {
            db.Set<TEntity>().Add(entity);
        }

        public void Delete(int Id)
        {
            var entity = db.Set<TEntity>().Find(Id);
            if (entity != null)
                db.Set<TEntity>().Remove(entity);
        }
        public void Update(TEntity entity)
        {
            db.Set<TEntity>().Update(entity);
        }
        public int Save()
        {
            int count=db.SaveChanges();
            return count;
        }

        public bool RemoveImage(string imageName)
        {
            try
            {
                string uploadsFolder = Path.Combine(webHost.WebRootPath, "Images");
                string filePath = Path.Combine(uploadsFolder, imageName);

                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                    return true; // Image successfully deleted
                }
                else
                {
                    return false; // Image not found
                }
            }
            catch (Exception ex)
            {
                // Handle exceptions, log them, or return an error indicator
                Console.WriteLine($"Error deleting image: {ex.Message}");
                return false;
            }
        }



        public Region_Sector_Details LastId()
        {
           return db.Region_Sector_Details.OrderBy(a => a.Id).LastOrDefault();
        }

      
    }
}
