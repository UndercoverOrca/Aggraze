using Aggraze.Domain;
using LanguageExt;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the gain/loss of the of a traders' taken (backtest) trades, expressed in %.
/// </summary>
public class Mutation : IInsight
{
    public string Name => "Mutation";
    
    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades)
    {
        return Prelude.None;
    }
}