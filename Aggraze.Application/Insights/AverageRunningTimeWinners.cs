using Aggraze.Domain;
using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a win.
/// </summary>
public class AverageRunningTimeWinners : IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeCalculator;

    public AverageRunningTimeWinners(IAverageRunningTimeCalculator averageRunningTimeCalculator) =>
        this._averageRunningTimeCalculator = averageRunningTimeCalculator;

    public string Name => "Average running time winners";

    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades) =>
        trades
            .All(ContainsRequiredValues)
            ? Some(this._averageRunningTimeCalculator.CalculateAverageRunningTime(Name, trades.Where(x => x.Data.Result == Result.Win)))
            : None;
    
    private static Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.OpenTime.IsSome
        && x.Data.ClosingTime.IsSome
        && x.Data.Result.IsSome;
}