using Convertor.Dijkstra.Graphing;
using Convertor.Dijkstra.Pathing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Converter.Converter
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private GraphBuilder _builder { get; set; }
        private List<Tuple<string, string, double>> _conversionRates { get; set; }

        public void ClearConfiguration()
        {
            _conversionRates = new List<Tuple<string, string, double>>();
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            if (amount == 0)
                return 0;

            var shortPath = GetPathsBetweenCurrencies(fromCurrency, toCurrency);
            return CalculateAmountByPath(shortPath, amount);
        }

        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            InitialBuilder();
            InitialConvertionRate();
            UpdateConvertionRate(conversionRates);
        }


        /// <summary>
        /// update conversionRates And Add those to graph if needed
        /// </summary>
        /// <param name="conversionRates">ConversionRates "SourceCurrency,DestinationCurrency,ExchangeRate"</param>
        private void UpdateConvertionRate(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            conversionRates = conversionRates.Select(a => a = new Tuple<string, string, double>(a.Item1.ToLower(), a.Item2.ToLower(), a.Item3)).ToList();

            foreach (var conversionRate in conversionRates)
            {
                if (_conversionRates.Any(a => a.Item1 == conversionRate.Item1 && a.Item2 == conversionRate.Item2))
                {
                    continue;
                }

                if (!_conversionRates.Any(a => a.Item1 == conversionRate.Item1 || a.Item2 == conversionRate.Item1))
                    _builder.AddNode(conversionRate.Item1);

                if (!_conversionRates.Any(a => a.Item1 == conversionRate.Item2 || a.Item2 == conversionRate.Item2))
                    _builder.AddNode(conversionRate.Item2);

                if (!_builder.CheckExistLink(conversionRate.Item1, conversionRate.Item2))
                    _builder.AddLink(conversionRate.Item1, conversionRate.Item2, 1);

                if (!_builder.CheckExistLink(conversionRate.Item2, conversionRate.Item1))
                    _builder.AddLink(conversionRate.Item2, conversionRate.Item1, 1);

                _conversionRates.Add(conversionRate);
            }
        }
        private void InitialBuilder()
        {
            if (_builder is null)
            {
                _builder = new GraphBuilder();
            }
        }
        private void InitialConvertionRate()
        {
            if (_conversionRates is null)
            {
                _conversionRates = new List<Tuple<string, string, double>>();
            }
        }

        private double CalculateAmountByPath(List<PathSegment> paths , double amount)
        {
            if (paths is null)
            {
                return 0;
            }


            foreach (var path in paths)
            {
                var rate1 = _conversionRates.FirstOrDefault(a => a.Item1 == path.Origin.Id && a.Item2 == path.Destination.Id);

                if (rate1 != null)
                {
                    amount *= rate1.Item3;
                    continue;
                }

                var rate2 = _conversionRates.FirstOrDefault(a => a.Item1 == path.Destination.Id && a.Item2 == path.Origin.Id);

                if (rate2 != null)
                {
                    amount /= rate2.Item3;
                    continue;
                }

                amount = 0;
                break;
            }

            return amount;
        }

        private List<PathSegment> GetPathsBetweenCurrencies(string fromCurrency , string toCurrency)
        {
            var graph = _builder.Build();
            var pathFinder = new PathFinder(graph);
            var path = pathFinder.FindShortestPath(graph.Nodes.Single(node => node.Id == fromCurrency.ToLower()),
                                                   graph.Nodes.Single(node => node.Id == toCurrency.ToLower()));
            return path.Segments.ToList();
        }

    }
}
