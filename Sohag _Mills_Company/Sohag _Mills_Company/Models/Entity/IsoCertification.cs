using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    //السـادة أعضــاء مجلس إدارة الشركة
    public class IsoCertification
    {
        public int Id { get; set; }
        [Display(Name = "اسم الشهادة ")]
        public string? Name { get; set; }

        [Display(Name = " نص   الشهادة")]
        public string? Description_Body { get; set; }
        [Display(Name = "أضافة صورة للشهادة")]
        public  string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? ImageIFormFile { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
