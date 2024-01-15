using System.ComponentModel.DataAnnotations;

namespace Sohag__Mills_Company.Models.Entity
{
    //هيكل المساهمين 
    public class Shareholder_Structure
    {
        public int?Id { get; set; }

        [Display(Name = "البيان")]
        public string? Statement { get; set; }

        [Display(Name = "عدد الاسهم")]
        public double? Number_Shares { get; set; }

        [Display(Name = "القيمة الدفترية بالجنية")]
        public double? Value_Bound { get; set; }

        [Display(Name = "النسية المؤية")]
        public double? Percentage { get; set; }
    }   
}
