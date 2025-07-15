using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a loss.
/// </summary>
public class AverageRunningTimeLosers : IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeLosersCalculator;

    public AverageRunningTimeLosers(IAverageRunningTimeCalculator averageRunningTimeLosersCalculator) =>
        this._averageRunningTimeLosersCalculator = averageRunningTimeLosersCalculator;

    public string Name => "Average running time losers";

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .Any(ContainsRequiredValues)
            ? Some(this._averageRunningTimeLosersCalculator.Calculate(Name, trades
                .Where(x => x.Data.Result == Result.Loss)
                .ToList()))
            : None;
    
    private static Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.OpenTime.IsSome
        && x.Data.ClosingTime.IsSome
        && x.Data.Result.IsSome;
}