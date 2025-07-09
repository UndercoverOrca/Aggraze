using System.Globalization;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class AverageRunningTimeCalculator : IAverageRunningTimeCalculator
{
    public InsightResult CalculateAverageRunningTime(string name, IEnumerable<TradeRow> trades)
    {
        var culture = CultureInfo.InvariantCulture;
            
        var groupedByYearAndMonth = trades
            .GroupBy(x => new { x.Date.Year, x.Date.Month })
            .ToDictionary(x => x.Key, x => x
                .Select(y => y.Data));
        
        var yearMonthData = new Dictionary<int, Dictionary<string, TimeSpan>>();
        var summaryHelper = new Dictionary<string, List<TimeSpan>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var averageDuration = group.Value
                .Select(trade => trade.ClosingTime.Value() - trade.OpenTime.Value())
                .Average(timeSpan => timeSpan.Ticks);

            var averageDurationAsTimeSpan = TimeSpan.FromTicks((long)averageDuration);

            var year = group.Key.Year;
            var month= culture.DateTimeFormat.GetMonthName(group.Key.Month);

            if (!yearMonthData.ContainsKey(year))
            {
                yearMonthData[year] = new Dictionary<string, TimeSpan>();
            }
            
            yearMonthData[year][month] = averageDurationAsTimeSpan;
            
            if (!summaryHelper.ContainsKey(month))
            {
                summaryHelper[month] = [];
            }
            
            summaryHelper[month].AddRange(averageDurationAsTimeSpan);
        }

        var summary = new Summary(
            SummaryType.Average,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => TimeSpan.FromTicks((long)x.Value.Average(d => d.Ticks))
            ));

        var orderedYearMonthData = yearMonthData
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        return new InsightResult(name, orderedYearMonthData, summary);
    }
}