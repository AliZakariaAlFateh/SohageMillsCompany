using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    //المؤشـرات الماليـة والاقتصـاديـة للشركـة
    public class Indicators_Details
    {
        public int? id { get; set; }
        [Display(Name ="السنة البيان")]
        public DateTime? Year_Statement { get; set; }
        [Display(Name = "كمية البيان")]
        public string? Quentity_Year { get; set; }
        [Display(Name = "البيان")]
        [ForeignKey("Statement")]
        public int? StatementId { get; set; }
        public Statement? Statements { get; set; }
    }
}
