using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    //الخطط المستقبلية
    public class FuturePlan
    {
        public int? Id { get; set; }
        [Display(Name = "اسم الخطة")]
        public string? Plan_Name { get; set; }
        [Display(Name = "سنة الخطة")]
        public DateTime? Plane_Year { get; set; }
        public List<FuturePlan_Details>? PlanDetails { get; set; }


    }
}
