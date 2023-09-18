using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels {
    public class Message {
        public int ID { get; set; }

        [StringLength(100)]
        [Display(Name = "موضوع")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        public string Subject { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "لطفاً ایمیل معتبر وارد نمایید")]
        public string Email { get; set; }

        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [Display(Name = "متن پیام")]
        [StringLength(500, ErrorMessage = "نمی تواند بیش از 500 کاراکتر باشد")]
        public string Text { get; set; }
        public DateTime Date { get; set; }
    }
}