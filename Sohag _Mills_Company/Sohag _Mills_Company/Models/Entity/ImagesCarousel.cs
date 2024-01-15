using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    //السـادة أعضــاء مجلس إدارة الشركة
    public class ImagesCarousel
    {
        public int Id { get; set; }
        [Display(Name = "اسم الصورة ")]
        public string? Name { get; set; }

        [Display(Name = " نص الصورة أو الوصف")]
        public string? Description_Body { get; set; }
        [Display(Name = "أضافة صورة")]
        public  string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? ImageIFormFile { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
