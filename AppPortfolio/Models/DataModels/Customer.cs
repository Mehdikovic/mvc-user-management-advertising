using AppPortfolio.Models.DataModels.CustomValidations;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models
{
    public class Customer
    {
        public Customer()
        {
            Customer_Question = new List<Junc_Customer_Question>();
        }
        public int ID { get; set; }

        [StringLength(100)]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "لطفاً ایمیل معتبر وارد نمایید")]
        [CustomerEmailValidation(ErrorMessage = "این ایمیل قبلاً ثبت شده است")]
        public string Email { get; set; }
        [StringLength(13, MinimumLength = 13)]
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-[0-9]{3}-[0-9]{4}", ErrorMessage = "09xx-xxx-xxxx")]
        [CustomerPhoneValidation(ErrorMessage = "این موبایل قبلاً ثبت شده است")]
        public string Phone { get; set; }

        public DateTime DateOfRegister { get; set; }

        public virtual ICollection<Junc_Customer_Question> Customer_Question { get; set; }
    }
}