using Sohag__Mills_Company.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.ViewModels
{
    public class FuturePlanViewModel
    {

        public FuturePlan FuturePlan { get; set; }
        public List<FuturePlan_Details> FuturePlanDetails { get; set; }
    }
}
