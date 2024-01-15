using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.Models.Entity
{
    public class Statement
    {
        public int? Id { get; set; }
        [Display(Name = "البيان")]
        public string? Statement_Name { get; set; }
        [ForeignKey("Indicators")]
        [Display(Name = "المؤشر")]
        public int? IndicatorId { get; set; }
        public Indicators? Indicators { get; set; }

        public List<Indicators_Details>? indicators_Details { get; set; }

    }
}
