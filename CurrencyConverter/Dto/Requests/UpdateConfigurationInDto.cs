namespace CurrencyConverter.Dto.Requests
{
    public class UpdateConfigurationInDto
    {
        public List<CurrenciesType> currencyConfig { get; set; }
    }

    public class CurrenciesType
    {
        public string Source { get; set; }
        public string Destination { get; set; }
        public double Rate { get; set; }

    }
}
