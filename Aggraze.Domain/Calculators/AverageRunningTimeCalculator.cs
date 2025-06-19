using System.Globalization;

namespace Aggraze.Domain.Calculators;

public class AverageRunningTimeCalculator : IAverageRunningTimeCalculator
{
    private static readonly IReadOnlyList<string> NecessaryHeaders = [];

    public InsightResult CalculateAverageRunningTime(string name, IEnumerable<TradeRow> trades)
    {
        var groupedByYearAndMonth = trades
            .GroupBy(x => new { x.Date.Year, x.Date.Month })
            .ToDictionary(x => x.Key, x => x
                .Select(y => y.Value));
        
        var yearMonthData = new Dictionary<int, Dictionary<string, TimeSpan>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var x = group.Value
                .Select(trade => TimeSpan.Parse(trade["Closing time"]) - TimeSpan.Parse(trade["Open time"]))
                .Select(x => x.TotalMilliseconds);
        }

        return new InsightResult("beep", new Dictionary<int, Dictionary<string, TimeSpan>>(), new Dictionary<int, TimeSpan>());
    }
}