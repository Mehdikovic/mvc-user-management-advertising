using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels.SearchViewModels {
    public class NewsSearchViewModel {
        public HiringNews News { get; set; }
        public IEnumerable<HiringNews> NewsList { get; set; }
    }
}