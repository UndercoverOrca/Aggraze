using Aggraze.Domain;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades.
/// </summary>
public class AverageRunningTime : IInsight
{
    public string Name => "Average running time";
    public InsightResult GenerateInsight(IEnumerable<TradeData> trades)
    {
        throw new NotImplementedException();
    }
}