using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Sklep
{
    class RegisterValidation : ValidationRule
    {
        public string ErrorMessage { get; set; }
        public string RequiredString { get; set; }
        public RegisterValidation()
        {

        }

        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            if (value.ToString() == String.Empty)
                return new ValidationResult(false, "Wypełnij to pole");
            if (!value.ToString().Contains(RequiredString))
                return new ValidationResult(false, ErrorMessage);
            else
                return ValidationResult.ValidResult;
        }
    }
}
