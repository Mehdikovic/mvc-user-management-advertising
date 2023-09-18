using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class AIOViewModel {
        public IEnumerable<App> Apps { get; set; }
        public IEnumerable<Comment> Comments { get; set; }
        public IEnumerable<HiringNews> News { get; set; }
        public IEnumerable<Question> Questions { get; set; }
    }
}