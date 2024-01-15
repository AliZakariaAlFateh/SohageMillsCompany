using System.ComponentModel.DataAnnotations;

namespace Sohag__Mills_Company.Models.Entity
{
    public class Sector
    {
        public int Id { get; set; }
        [Display(Name = "اسم القطاع")]
        public string? Sector_Name { get; set; }
        public List<Region_Sector_Details>? Region_Sector_Details { get; set; }
    }



}
