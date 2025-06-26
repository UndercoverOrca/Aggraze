using Aggraze.Domain;
using LanguageExt;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a loss.
/// </summary>
public class AverageRunningTimeLosers : IInsight
{
    public string Name => "Average running time losers";
    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades)
    {
        return Prelude.None;
    }
}