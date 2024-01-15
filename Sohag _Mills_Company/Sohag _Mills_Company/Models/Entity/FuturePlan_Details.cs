using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    public class FuturePlan_Details
    {
        public int? Id { get; set; }
        public string? Plan_Text { get; set; }
        [ForeignKey("FuturePlan")]
        public int? FuturePlanId { get; set; }
        public FuturePlan? FuturePlan { get; set; }

    }
}
