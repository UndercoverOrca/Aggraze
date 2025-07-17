using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades.
/// </summary>
public class AverageRunningTime : InsightBase, IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeCalculator;

    public AverageRunningTime(IAverageRunningTimeCalculator averageRunningTimeCalculator) =>
        this._averageRunningTimeCalculator = averageRunningTimeCalculator;

    public string Name => "Average running time";

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .Any(ContainsRequiredValues)
            ? Some(Calculate(trades))
            : None;

    private static Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.OpenTime.IsSome
        && x.Data.ClosingTime.IsSome;

    private IInsightResult Calculate(IReadOnlyList<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, TimeSpan>>();
        var summaryHelper = new Dictionary<string, List<TimeSpan>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var averageDurationAsTimeSpan = this._averageRunningTimeCalculator.Calculate(group);
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, averageDurationAsTimeSpan);
        }

        var summary = new Summary<TimeSpan>(
            SummaryType.Average,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => TimeSpan.FromTicks((long)x.Value.Average(d => d.Ticks))
            ));

        var orderedYearMonthData = OrderYearMonthDataByYear(yearMonthData);
        return new InsightResult<TimeSpan>(Name, orderedYearMonthData, summary);
    }
}