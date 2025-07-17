using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;

using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum Risk:Reward ratio from a traders' taken (backtest) trades where all the winning trades will still have won
/// </summary>
public class MaximumRiskRewardWinningTrades : InsightBase, IInsight
{
    private readonly IMaximumRiskRewardWinningTradesCalculator _maximumRiskRewardWinningTradesCalculator;

    public MaximumRiskRewardWinningTrades(IMaximumRiskRewardWinningTradesCalculator maximumRiskRewardWinningTradesCalculator) =>
        _maximumRiskRewardWinningTradesCalculator = maximumRiskRewardWinningTradesCalculator;

    public string Name => "Max RR where all trades would still win";
    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
    trades
        .Any(ContainsRequiredValues)
    ? Some(Calculate(trades
        .Where(ContainsRequiredValues)
        .ToList()))
    : None;
    
    private Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.MaximumResult.IsSome
        && x.Data.Result.IsSome;
    
    private IInsightResult Calculate(IReadOnlyList<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var maximumDrawdown = this._maximumRiskRewardWinningTradesCalculator.Calculate(group);
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, maximumDrawdown);
        }

        var summary = new Summary<decimal>(
            SummaryType.Maximum,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => x.Value.Max()));

        var orderedYearMonthData = yearMonthData
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        return new InsightResult<decimal>(Name, orderedYearMonthData, summary);
    }
}