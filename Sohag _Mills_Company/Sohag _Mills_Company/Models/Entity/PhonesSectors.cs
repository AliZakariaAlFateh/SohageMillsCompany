using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    public class PhonesSectors
    {
        public int Id { get; set; }

        [Display(Name = "رقم تليفون المنطقة")]

        public string? PhoneNumber { get; set; }
        [ForeignKey("Phones_Address_Details")]
        public int? Phones_Address_DetailsId { get; set; }
        public Region_Sector_Details? Phones_Address_Details { get; set; }

    }



}
