using System.ComponentModel.DataAnnotations;

namespace Sohag__Mills_Company.Models.Entity
{

    //المؤشـرات الماليـة والاقتصـاديـة للشركـة
    public class Indicators
    {
        public int? Id { get; set; }
        
        [Display(Name = "المؤشر")]
        public string? Type_Indicator { get; set; }
        public List<Statement>? Statements { get; set; }


    }
}
