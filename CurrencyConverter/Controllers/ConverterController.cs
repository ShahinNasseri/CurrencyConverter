using Converter.Converter;
using CurrencyConverter.Dto.Requests;
using Microsoft.AspNetCore.Mvc;

namespace CurrencyConverter.Controllers
{
    [Route("Api/[controller]/[action]")]
    [ApiController]
    public class ConverterController : ControllerBase
    {
        private ICurrencyConverter _currencyConverter { get; set; }
        public ConverterController(ICurrencyConverter currencyConverter)
        {
            _currencyConverter = currencyConverter;
        }

        [HttpPost]
        public IActionResult Convert(ConvertInDto request)
        {

            lock (_currencyConverter)
            {
                var res = _currencyConverter.Convert(request.SourceCurrency, request.DestinationCurrency, request.Amount);

                if (res != 0)
                {
                    return Ok(res);
                }
                else
                {
                    return BadRequest("Something is wrong!");
                }
            }
           
        }

        [HttpPost]
        public IActionResult UpdateConfiguration(UpdateConfigurationInDto request)
        {
            var input = request.currencyConfig.Select(a => new Tuple<string, string, double>(a.Source, a.Destination, a.Rate)).ToList();

            lock (_currencyConverter)
            {
                _currencyConverter.UpdateConfiguration(input);
                return Ok();
            }
           
        }

        [HttpGet]
        public IActionResult ClearConfiguration()
        {
            _currencyConverter.ClearConfiguration();
            return Ok();
        }
    }
}
