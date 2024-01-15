using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    public class FilePdf
    {
        public int Id { get; set; }
        [Display(Name = "وصف الملف ")]
        //[Required(ErrorMessage ="الرجاء أختار نوع الملف")]
        public string? FileDescription { get; set; }
        [Required(ErrorMessage = "الرجاء أختار نوع الملف")]
        public int type_pdf { get; set; }    
        [Display(Name = "اسم الصورة ")]
        public string? ImageName { get; set; }
        [NotMapped]
        [Display(Name = "صورة الملف ")]
        public IFormFile? ImageIFormFile { get; set; }
        [Display(Name = "اسم الملف ")]
        public string? PdfName { get; set; }
        [NotMapped]
        [Display(Name = "أضافة الملف ")]
       // [RegularExpression(@"^.*\.pdf$", ErrorMessage = "Only PDF files are allowed.")]
        public IFormFile? PdfIFormFile { get; set; }
        public bool IsDeleted { get; set; } = false;
    }
}
