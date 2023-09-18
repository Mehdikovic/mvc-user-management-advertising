using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels.SearchViewModels {
    public class CommentSearchViewModel {
        public Comment Comment { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
    }
}