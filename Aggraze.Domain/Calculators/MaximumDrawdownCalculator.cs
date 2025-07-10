using System.Globalization;
using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumDrawdownCalculator : IMaximumDrawdownCalculator
{
    public InsightResult Calculate(string name, IEnumerable<TradeRow> trades)
    {
        var culture = CultureInfo.InvariantCulture;
            
        var groupedByYearAndMonth = trades
            .GroupBy(x => new { x.Date.Year, x.Date.Month })
            .ToDictionary(x => x.Key, x => x
                .Select(y => y.Data));
        
        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var maximumDrawdown = group.Value
                .Select(trade => trade.MaximumDrawdown)
                .Max(x => x.ValueUnsafe());
            
            var year = group.Key.Year;
            var month= culture.DateTimeFormat.GetMonthName(group.Key.Month);

            if (!yearMonthData.ContainsKey(year))
            {
                yearMonthData[year] = new Dictionary<string, decimal>();
            }
            
            yearMonthData[year][month] = maximumDrawdown;
            
            if (!summaryHelper.ContainsKey(month))
            {
                summaryHelper[month] = [];
            }
            
            summaryHelper[month].AddRange(maximumDrawdown);
        }

        var summary = new Summary(
            SummaryType.Average,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => x
            ));
        
        var orderedYearMonthData = yearMonthData
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);
        
        var x = new InsightResult(name, orderedYearMonthData, summary);
    }
}