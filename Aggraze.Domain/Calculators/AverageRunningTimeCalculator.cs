using System.Globalization;

namespace Aggraze.Domain.Calculators;

public class AverageRunningTimeCalculator : IAverageRunningTimeCalculator
{
    public InsightResult CalculateAverageRunningTime(string name, IEnumerable<TradeRow> trades)
    {
        var culture = CultureInfo.InvariantCulture;
            
        var groupedByYearAndMonth = trades
            .GroupBy(x => new { x.Date.Year, x.Date.Month })
            .ToDictionary(x => x.Key, x => x
                .Select(y => y.Value));
        
        var yearMonthData = new Dictionary<int, Dictionary<string, TimeSpan>>();
        var summaryHelper = new Dictionary<string, List<TimeSpan>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var averageDuration = group.Value
                .Select(trade => TimeSpan.Parse(trade["Closing time"]) - TimeSpan.Parse(trade["Open time"]))
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
                summaryHelper[month] = new List<TimeSpan>();
            }
            
            summaryHelper[month].AddRange(averageDurationAsTimeSpan);
        }
        
        var summary = summaryHelper.ToDictionary(
            kvp => kvp.Key,
            kvp => TimeSpan.FromTicks((long)kvp.Value.Average(d => d.Ticks))
        );

        return new InsightResult(name, yearMonthData, summary);
    }
}