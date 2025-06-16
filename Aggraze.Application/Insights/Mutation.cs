using Aggraze.Domain;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the gain/loss of the of a traders' taken (backtest) trades, expressed in %.
/// </summary>
public class Mutation : IInsight
{
    public string Name => "Mutation";
    
    public InsightResult GenerateInsight(IEnumerable<TradeRow> trades)
    {
        throw new NotImplementedException();
    }
}