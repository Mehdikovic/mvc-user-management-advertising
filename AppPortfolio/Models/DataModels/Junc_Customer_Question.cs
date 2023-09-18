using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models
{
    public class Junc_Customer_Question
    {
        public int ID { get; set; }

        
        public int CustomerID { get; set; }

        public int QuestionID { get; set; }

        [ForeignKey("CustomerID")]
        public virtual Customer Customer { set; get; }

        [ForeignKey("QuestionID")]
        public virtual Question Question { set; get; }

        public DateTime DateOfDownload { get; set; }

    }
}