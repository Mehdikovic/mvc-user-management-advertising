using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models
{
    public class App
    {
        public App()
        {
            App_Feature = new List<Junc_App_Feature>();
        }
        public int ID { get; set; }

        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [StringLength(50)]
        [Display(Name = "نام اپلیکیشن")]
        public string Name { get; set; }
        [Display(Name = "مسیر اپلیکیشن آپلود شده")]
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        public string Filepath { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreatedDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime UpdatedDate { get; set; }
        [Display(Name = "مسیر عکس آپلود شده")]
        public string Imagepath { get; set; }
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [Column(TypeName = "ntext")]
        [Display(Name = "لیست تگ")]
        [MaxLength]
        public string Tags { get; set; }
        [ScaffoldColumn(false)]
        public WorkType Type { get; set; }
        public virtual ICollection<Junc_App_Feature> App_Feature { get; set; }
    }
}