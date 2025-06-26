using Aggraze.Domain;
using LanguageExt;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a win.
/// </summary>
public class AverageRunningTimeWinners : IInsight
{
    public string Name => "Average running time winners";
    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades)
    {
        return Prelude.None;
    }
}