using Convertor.Dijkstra.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CurrencyConverter.Filters
{
    public class ApiExceptionFilter: ExceptionFilterAttribute
    {
        private readonly IDictionary<Type, string> _exceptiontypes;

        public ApiExceptionFilter()
        {
            _exceptiontypes = new Dictionary<Type, string>
            {
                { typeof(GraphBuilderException), "Something is wrong" },
                { typeof(NoPathFoundException), "There is no way convert this currencies" },
                { typeof(PathFinderException), "Something is wrong" },
            };
        }

        public override void OnException(ExceptionContext context)
        {
            HandleException(context);

            base.OnException(context);
        }

        private void HandleException(ExceptionContext context)
        {
            Type type = context.Exception.GetType();
            if (_exceptiontypes.ContainsKey(type))
            {
                context.Result = new OkObjectResult(_exceptiontypes[type]);
                return;
            }

            context.Result = new BadRequestObjectResult("Something is wrong");
        }
    }
}
