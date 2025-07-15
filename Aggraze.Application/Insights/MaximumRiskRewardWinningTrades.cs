using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;

using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum Risk:Reward ratio from a traders' taken (backtest) trades where all the winning trades will still have won
/// </summary>
public class MaximumRiskRewardWinningTrades : IInsight
{
    private readonly IMaximumRiskRewardWinningTradesCalculator _maximumRiskRewardWinningTradesCalculator;

    public MaximumRiskRewardWinningTrades(IMaximumRiskRewardWinningTradesCalculator maximumRiskRewardWinningTradesCalculator) =>
        _maximumRiskRewardWinningTradesCalculator = maximumRiskRewardWinningTradesCalculator;

    public string Name => "Max RR where all trades would still win";
    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
    trades
        .Any(ContainsRequiredValues)
    ? Some(_maximumRiskRewardWinningTradesCalculator.Calculate(Name, trades.Where(ContainsRequiredValues).ToList()))
    : None;
    
    private Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.MaximumResult.IsSome
        && x.Data.Result.IsSome;
}