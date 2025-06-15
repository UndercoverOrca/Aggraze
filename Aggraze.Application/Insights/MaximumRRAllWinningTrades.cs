using Aggraze.Domain;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum Risk:Reward ratio from a traders' taken (backtest) trades where all the winning trades will still have won
/// </summary>
public class MaximumRRAllWinningTrades : IInsight
{
    public string Name => "Maximum RR where all trades would still win";
    public InsightResult GenerateInsight(IEnumerable<TradeData> trades)
    {
        throw new NotImplementedException();
    }
}