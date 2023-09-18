using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models
{
    public class Feature
    {
        public Feature()
        {
            App_Feature = new List<Junc_App_Feature>();
        }
        public int ID { get; set; }
        [Required(ErrorMessage = "نمی تواند خالی باشد")]
        [StringLength(50)]
        [Display(Name = "تیتر خصوصیت")]
        public string Title { get; set; }

        [StringLength(500, ErrorMessage = "طول بیش از 500 کارکتر مجاز نیست")]
        [Display(Name = "متن")]
        public string Text { get; set; }

        [StringLength(100, ErrorMessage = "طول بیش از 100 کارکتر مجاز نیست")]
        [Display(Name = "نام آیکن")]
        public string GlyphIconName { get; set; }
        public virtual ICollection<Junc_App_Feature> App_Feature { get; set; }
    }
}