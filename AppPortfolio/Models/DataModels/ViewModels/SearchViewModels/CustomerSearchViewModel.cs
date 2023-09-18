using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.ViewModels.SearchViewModels {
    public class CustomerSearchViewModel {
        public Customer Customer { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}