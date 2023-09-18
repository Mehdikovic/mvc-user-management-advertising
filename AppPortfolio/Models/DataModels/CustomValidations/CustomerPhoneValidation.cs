using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.CustomValidations {
    public class CustomerPhoneValidation : ValidationAttribute {
        ApplicationDbContext context;
        public CustomerPhoneValidation() : base() {
            context = new ApplicationDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (context.Customer.Any(m => m.Phone == ((string)value).Trim()))
                return new ValidationResult("این موبایل قبلاً ثبت شده است");
            return null;
        }
    }
}