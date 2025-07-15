using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumRiskRewardWinningTradesCalculator
{
    IInsightResult Calculate(string name, IReadOnlyList<TradeRow> trades);
}