using System.ComponentModel.DataAnnotations;

namespace Sohag__Mills_Company.Models.Entity
{
    public class clients
    {
        public int Id { get; set; }
        [Display(Name = " كود العميل")]
        public long? Clint_Code { get; set; }
        [Display(Name = " أسم العميل")]
        public string? Clint_Name { get; set; }
        [Display(Name = " عنوان العميل")]
        public string? Clint_Adress { get; set; }
        
        [Display(Name = "الجنسية")]
        public string? Nationaity {get;set;}
        [Display(Name = "الرصيد")]
        public long? Clint_Balance { get; set; }
        [Display(Name = "شراء تحت التسوية")]
        public int? Purchase_under_settlement { get; set; }
        [Display(Name = "بيع تحت التسوية")]
        public int? sale_under_settlement { get; set; }
        [Display(Name = "نسبة المساهمة")]
        public double? Contribution_percentage { get; set; }

    }
}
