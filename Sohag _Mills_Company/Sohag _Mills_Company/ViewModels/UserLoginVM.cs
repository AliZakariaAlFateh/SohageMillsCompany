using Sohag__Mills_Company.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.ViewModels
{
    public class UserLoginVM
    {

        [Display(Name = "الاسم")]
        public string UserName { get; set; }
        [DataType(DataType.Password)]
        [Display(Name = "الرقم السرى")]
        public string Password { get; set; }
        [Display(Name = "تذكرنى")]
        public bool RememberMe { get; set; }
    }
}
