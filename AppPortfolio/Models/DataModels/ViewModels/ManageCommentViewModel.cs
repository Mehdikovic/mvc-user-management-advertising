using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class ManageCommentViewModel {
        public HiringNews News { get; set; }
        public List<Comment> Comments { get; set; }
    }
}