using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class NewsViewModel {

        public string YearMonth { get; set; }

        public List<NewsInDaysViewModel> NewsInDays { get; set; }
    }

    public class NewsInDaysViewModel {
        public string Day { get; set; }
        public int Count { get; set; }
    }
}