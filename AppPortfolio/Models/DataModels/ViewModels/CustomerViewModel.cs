using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class CustomerViewModel {
        public Customer Customer { get; set; }
        public CustomerLogin CustomerLogin { get; set; }
    }

    public class CustomerLogin {
        [StringLength(100)]
        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "لطفاً ایمیل معتبر وارد نمایید")]
        public string Email { get; set; }
        [StringLength(13, MinimumLength = 13)]
        [Display(Name = "موبایل")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [RegularExpression("09(1[0-9]|3[1-9]|2[1-9])-[0-9]{3}-[0-9]{4}", ErrorMessage = "09xx-xxx-xxxx")]
        public string Phone { get; set; }
    }
}