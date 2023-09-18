using System.Collections.Generic;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class CommentViewModel {
        public Comment ParentComment { get; set; }
        public List<Comment> ChildComments { get; set; }
    }
}