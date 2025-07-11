using Aggraze.Domain;
using LanguageExt;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum drawdown of the traders' taken (backtest) trades that resulted in a win.
/// </summary>
public class MaximumDrawdown : IInsight
{
    public string Name => "Maximum drawdown";
    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades)
    {
        return Prelude.None;
    }
}