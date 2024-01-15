using Sohag__Mills_Company.Models.Entity;

namespace Sohag__Mills_Company.Services
{
    public interface IRegionRepo
    {
        IEnumerable<Region_Sector_Details> GetAll();
        Region_Sector_Details GetByID(int? Id);
    }
}