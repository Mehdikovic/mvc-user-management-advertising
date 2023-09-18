using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppPortfolio.Models.DataModels.CustomValidations {
    public class CustomerEmailValidation : ValidationAttribute {

        ApplicationDbContext context;
        public CustomerEmailValidation() : base() {
            context = new ApplicationDbContext();
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext) {
            if (context.Customer.Any(m => m.Email == ((string)value).Trim()))
                return new ValidationResult("این ایمیل قبلاً ثبت شده است");
            return null;
        }
    }
}
