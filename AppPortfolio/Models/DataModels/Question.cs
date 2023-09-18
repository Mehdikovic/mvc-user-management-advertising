using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace AppPortfolio.Models {
    public class Question {
        public Question() {
            Customer_Question = new List<Junc_Customer_Question>();
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [StringLength(50)]
        [Display(Name = "نام سوال")]
        public string Name { get; set; }
        [Display(Name = "مسیر سوال آپلود شده")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        public string Filepath { get; set; }
        public DateTime CreatedDate { get; set; }
        public WorkType Type { get; set; }
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [Column(TypeName = "ntext")]
        [Display(Name = "لیست تگ")]
        [MaxLength]
        public string Tags { get; set; }
        [DataType(DataType.Html)]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [AllowHtml]
        [Column(TypeName = "ntext")]
        [Display(Name = "توضیحات خبر")]
        [MaxLength]
        public string Description { get; set; }

        public virtual ICollection<Junc_Customer_Question> Customer_Question { get; set; }
    }
}