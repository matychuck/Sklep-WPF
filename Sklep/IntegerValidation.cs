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
    class IntegerValidation : ValidationRule
    {
        public int? MinValue { get; set; }
        public int? MaxValue { get; set; }
        public string ErrorMessage { get; set; }

        private static ProductWindow product_window;
        public IntegerValidation()
        {
            foreach (Window w in Application.Current.Windows)
            {
                if (w.Title == "ProductWindow")
                    product_window = (ProductWindow)w;
            }
        }
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            int i;

            if (!int.TryParse(value.ToString(), out i))
                return new ValidationResult(false, "Podana wartość nie jest liczbą!");
            else
                 if (i < (MinValue ?? i))
                return new ValidationResult(false, ErrorMessage);
            else if (i > product_window.availableAmount[product_window.SizeBox.SelectedItem.ToString()].Amount)
                return new ValidationResult(false, "Wartość nie może być większa od " + product_window.availableAmount[product_window.SizeBox.SelectedItem.ToString()].Amount );
            else
                return ValidationResult.ValidResult;
        }
    }
}
