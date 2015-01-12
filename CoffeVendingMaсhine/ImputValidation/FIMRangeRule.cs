using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Controls;

namespace CoffeVendingMaсhine.ImputValidation
{
	public class FIMRangeRule : ValidationRule
	{

		public override ValidationResult Validate(object value, System.Globalization.CultureInfo cultureInfo)
		{
			if (value == null || string.IsNullOrEmpty(value.ToString()))
				return new ValidationResult(false, "Feltet mе ikke vжre tomt. Indtast gyldig vжrdi.");
			else
			{

				if ((Int32.Parse(value.ToString()) < 18) || (Int32.Parse(value.ToString()) > 126))
					return new ValidationResult
						(false, "Vжrdi udenfor gyldig interval 18-126");
			}
			return ValidationResult.ValidResult;
		}
	}
}
