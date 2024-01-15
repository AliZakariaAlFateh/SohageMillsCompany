using Sohag__Mills_Company.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.ViewModels
{
    public class FuturePlanDTO
    {

        public int? Id { get; set; }
        public string? Plan_Name { get; set; }
        public DateTime? Plane_Year { get; set; }
        public List<FuturePlanDetailsDTO>? PlanDetails { get; set; }
    }
}
