using Sohag__Mills_Company.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.ViewModels
{
    public class Sectors_RegionVM
    {
        public Sectors_RegionVM()
        {
            Phonessectors = new List<string>();
        }
        public int sector_Id { get; set; }
        // [Display(Name = "اسم القطاع")]
        //public Sector? sector { get; set; }
        //   public List<int>? areaId { get; set; }
        // public List<string>? Areaname { get; set; }
        //  public List<string>? AddressArea { get; set; }

        public List<string>? Phonessectors { get; set; } 
       // public List<int>? phonesareaid { get; set; }
       // public List<PhonesSectors>? PhonesSectors { get; set; }
        public Region_Sector_Details? Region { get; set; }
    }
}
