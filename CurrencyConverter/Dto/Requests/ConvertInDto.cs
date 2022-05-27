namespace CurrencyConverter.Dto.Requests
{
    public class ConvertInDto
    {
        public string SourceCurrency { get; set; }
        public string DestinationCurrency { get; set; }
        public double Amount { get; set; }
    }
}
