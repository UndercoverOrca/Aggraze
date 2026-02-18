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

    private IInsightResult Calculate(IReadOnlyList<TradeRow> trades) =>
        CalculateInsight(
            Name,
            trades,
            this._averageRunningTimeCalculator.Calculate,
            values => TimeSpan.FromTicks((long)values.Average(d => d.Ticks)),
            SummaryType.Average);
}