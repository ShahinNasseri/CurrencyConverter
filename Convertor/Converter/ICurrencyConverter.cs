using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Converter
{
    public interface ICurrencyConverter
    {
        /// <summary>
        /// Clears any prior configuration.
        /// </summary>
        void ClearConfiguration();
        /// <summary>
        /// Updates the configuration. Rates are inserted or replaced internally.
        /// </summary>
        void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates);

        /// <summary>
        /// Converts the specified amount to the desired currency.
        /// </summary>
        double Convert(string fromCurrency, string toCurrency, double amount);
    }
}
