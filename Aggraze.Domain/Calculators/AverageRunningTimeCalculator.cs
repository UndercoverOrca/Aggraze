using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class AverageRunningTimeCalculator : Calculator, IAverageRunningTimeCalculator
{
    public IInsightResult Calculate(string name, IEnumerable<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);
        
        var yearMonthData = new Dictionary<int, Dictionary<string, TimeSpan>>();
        var summaryHelper = new Dictionary<string, List<TimeSpan>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var averageDuration = group.Value
                .Select(trade => trade.ClosingTime.Value() - trade.OpenTime.Value())
                .Average(timeSpan => timeSpan.Ticks);

            var averageDurationAsTimeSpan = TimeSpan.FromTicks((long)averageDuration);

            //TODO: Check whether this is correct
            var x = TimeSpan.FromTicks(
                (long)group.Value
                    .Select(trade => trade.ClosingTime.Value() - trade.OpenTime.Value())
                    .Average(x => x.Ticks));
            
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, averageDurationAsTimeSpan);
        }
        
        var summary = new Summary(
            SummaryType.Average,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => TimeSpan.FromTicks((long)x.Value.Average(d => d.Ticks))
            ));

        var orderedYearMonthData = OrderYearMonthDataByYear(yearMonthData);

        return new InsightResult<TimeSpan>(name, orderedYearMonthData, summary);
    }
}