using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    public class Region_Sector_Details
    {
        public int Id { get; set; }
        [Display(Name = "أسم المنطقة")]
        public string? Area_Name { get; set; }
        [Display(Name = "عنوان المنطقة")]
        public string? Address_Name { get; set; }
        [ForeignKey("Sector")]
        public int SectorId { get; set; }
        public Sector? Sector { get; set; }
        public List<PhonesSectors>? PhonesSectors { get; set; }
    }



}
