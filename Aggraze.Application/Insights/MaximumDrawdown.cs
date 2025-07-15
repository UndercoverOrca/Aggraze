using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum drawdown of the traders' taken (backtest) trades that resulted in a win.
/// </summary>
public class MaximumDrawdown : IInsight
{
    private readonly IMaximumDrawdownCalculator _maximumDrawdownCalculator;

    public MaximumDrawdown(IMaximumDrawdownCalculator maximumDrawdownCalculator) =>
        this._maximumDrawdownCalculator = maximumDrawdownCalculator;

    public string Name => "Maximum drawdown";

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .Any(ContainsRequiredValues)
    ? Some(this._maximumDrawdownCalculator.Calculate(Name, trades.Where(ContainsRequiredValues).ToList()))
    : None;
    
    private static Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.MaximumDrawdown.IsSome;
}