using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels.SearchViewModels {
    public class AppSearchViewModel {
        public App App { get; set; }
        public IEnumerable<App> Apps { get; set; }
    }
}