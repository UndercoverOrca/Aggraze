using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a win.
/// </summary>
public class AverageRunningTimeWinners : InsightBase, IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeCalculator;

    public AverageRunningTimeWinners(IAverageRunningTimeCalculator averageRunningTimeCalculator) =>
        this._averageRunningTimeCalculator = averageRunningTimeCalculator;

    public string Name => "Average running time winners";

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .All(ContainsRequiredValues)
            ? Some(Calculate(trades
                .Where(x => x.Data.Result == Result.Win)
                .ToList()))
            : None;
    
    private static Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.OpenTime.IsSome
        && x.Data.ClosingTime.IsSome
        && x.Data.Result.IsSome;
    
    private IInsightResult Calculate(IReadOnlyList<TradeRow> trades) =>
        CalculateInsight(
            Name,
            trades,
            this._averageRunningTimeCalculator.Calculate,
            values => TimeSpan.FromTicks((long)values.Average(d => d.Ticks)),
            SummaryType.Average);
}