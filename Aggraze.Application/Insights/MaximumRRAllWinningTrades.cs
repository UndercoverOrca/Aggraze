using Aggraze.Domain.Types;
using LanguageExt;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum Risk:Reward ratio from a traders' taken (backtest) trades where all the winning trades will still have won
/// </summary>
public class MaximumRRAllWinningTrades : IInsight
{
    public string Name => "Maximum RR where all trades would still win";
    public Option<IInsightResult> GenerateInsight(IEnumerable<TradeRow> trades)
    {
        return Prelude.None;
    }
}