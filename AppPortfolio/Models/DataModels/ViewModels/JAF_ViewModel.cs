using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels {
    public class JAF_ViewModel {
        public App AppFound { get; set; }
        public IEnumerable<Junc_App_Feature> FeaturesAppHas { get; set; }
        public IEnumerable<Feature> FeaturesAppDoesntHas { get; set; }
    }
}