using System.ComponentModel.DataAnnotations;

namespace Sohag__Mills_Company.Models.Entity
{
    public class About_Company
    {
        public int Id { get; set; }
        [Display(Name = "عنوان النص")]
        public string? title { get; set; }
        [Display(Name = "نص النبذة عن الشركة")]
        public string? Description { get; set; }

    }
}
