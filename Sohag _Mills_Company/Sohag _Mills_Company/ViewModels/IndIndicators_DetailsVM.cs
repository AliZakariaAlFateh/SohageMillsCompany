using System.ComponentModel.DataAnnotations;

namespace Sohag__Mills_Company.ViewModels
{
    public class IndIndicators_DetailsVM
    {
        public int? id { get; set; }
        [Display(Name = "الموشر")]
        public string? Type_Indicator { get; set; }
        [Display(Name = "البيان")]

        public string? statement_Name { get; set; }
        [Display(Name = "السنة")]

        public int? Year_Statement { get; set;}
        [Display(Name = "الكمية")]

        public string? Quentity_Year { get; set; }
    }
}
