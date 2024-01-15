using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    //السـادة أعضــاء مجلس إدارة الشركة
    public class CampanyTeam
    {
        public int Id { get; set; }
        [Display(Name = "الأسم")]
        public string? Name { get; set; }
        [Display(Name = "شهرتة ")]
        public string? nickname { get; set; } = "لقبه";
        [Display(Name = "الجنسية ")]
        public string? Nationaity { get; set; }
        [Display(Name = "الصفة")]
        public string? Postion { get; set; }
        public int? postionId { get; set; }
        [Display(Name = "جهة التمثيل")]
        public string? Representation_Body { get; set; }
        [Display(Name = "أضافة صورة")]
        public  string? ImageName { get; set; }
        [NotMapped]
        public IFormFile? ImageIFormFile { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
