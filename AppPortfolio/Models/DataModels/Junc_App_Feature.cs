using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models
{
    public class Junc_App_Feature
    {
        public int ID { get; set; }

        public int AppID { get; set; }
        
        public int FeatureID { get; set; }

        [ForeignKey("AppID")]
        public virtual App Application { set; get; }

        [ForeignKey("FeatureID")]
        public virtual Feature Feature { set; get; }
    }
}