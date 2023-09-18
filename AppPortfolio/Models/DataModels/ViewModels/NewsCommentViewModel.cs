using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class NewsCommentViewModel {
        public HiringNews News { get; set; }
        public Comment Comment { get; set; }

        public int ValidCommentCount { get; set; }
        public int InValidCommentCount { get; set; }
        public int ResponseCommentCount { get; set; }

        public IEnumerable<CommentViewModel> Comments { get; set; }
    }
}