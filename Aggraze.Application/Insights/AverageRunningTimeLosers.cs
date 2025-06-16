using Aggraze.Domain;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a loss.
/// </summary>
public class AverageRunningTimeLosers : IInsight
{
    public string Name => "Average running time losers";
    public InsightResult GenerateInsight(IEnumerable<TradeRow> trades)
    {
        throw new NotImplementedException();
    }
}