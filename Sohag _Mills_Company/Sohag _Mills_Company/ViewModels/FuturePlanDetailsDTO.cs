using Sohag__Mills_Company.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.ViewModels
{
    public class FuturePlanDetailsDTO
    {

        public int? Id { get; set; }
        public string? Plan_Text { get; set; }

        //public int? FuturePlanId { get; set; }
    }
}
