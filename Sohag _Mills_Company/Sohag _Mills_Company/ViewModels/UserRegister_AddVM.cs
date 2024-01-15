using Sohag__Mills_Company.Models.Entity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sohag__Mills_Company.ViewModels
{
    public class UserRegister_AddVM
    {
        [Display(Name = "الاسم")]
        public string UserName { get; set; }

        [Display(Name = "الرقم السرى")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = " الإيميل")]
        public string Email { get; set; }

        [Display(Name = " تأكيد الرقم السرى")]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Password and Confirm Password Not Matched")]
        public string ConfirmPassword { get; set; }

        [Display(Name = " اختر الصلاحية")]
        public string RoleName { get; set; }


    }
}
