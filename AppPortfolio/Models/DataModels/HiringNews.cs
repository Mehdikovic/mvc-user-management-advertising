using AppPortfolio.Models.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Models
{
    public class HiringNews
    {
        public HiringNews() {
            Comments = new List<Comment>();
        }
        public int ID { get; set; }

        [StringLength(100)]
        [Display(Name = "تیتر خبر")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        public string Name { get; set; }
        public string Date { get; set; }
        [Display(Name = "مسیر عکس آپلود شده")]
        public string Imagepath { get; set; }

        [DataType(DataType.Html)]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Display(Name = "متن خبر")]
        [MaxLength]
        public string Text { get; set; }
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [Column(TypeName = "ntext")]
        [MaxLength]
        [Display(Name = "لیست تگ")]
        public string Tags { get; set; }
        public virtual ICollection<Comment> Comments { set; get; }
    }
}