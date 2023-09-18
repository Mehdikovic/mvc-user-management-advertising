using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels {
    public class Comment {
        public int ID { get; set; }
        public int ParantID { get; set; }
        public int NewsID { get; set; }

        [StringLength(100)]
        [Display(Name = "نام شما")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        public string Name { get; set; }

        [Display(Name = "ایمیل")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [RegularExpression(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$", ErrorMessage = "لطفاً ایمیل معتبر وارد نمایید")]
        public string Email { get; set; }

        [ForeignKey("NewsID")]
        public virtual HiringNews News { get; set; }

        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [StringLength(500, ErrorMessage = "نمی تواند بیش از 500 کاراکتر باشد")]
        public string Text { get; set; }
        public DateTime Date { get; set; }

        public bool IsValid { get; set; }

    }
}