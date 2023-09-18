using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels.SearchViewModels {
    public class QuestionSearchViewModel {
        public Question Question { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}