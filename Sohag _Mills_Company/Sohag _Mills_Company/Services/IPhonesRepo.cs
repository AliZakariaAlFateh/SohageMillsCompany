using Sohag__Mills_Company.Models.Entity;

namespace Sohag__Mills_Company.Services
{
    public interface IPhonesRepo
    {
        IEnumerable<PhonesSectors> GetAll();
        public IEnumerable<Sector> GetAll_Sectors_With_Region_phones();
        PhonesSectors GetByID(int? Id);
    }
}