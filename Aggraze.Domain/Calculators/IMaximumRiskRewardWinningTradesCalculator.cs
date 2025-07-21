using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumRiskRewardWinningTradesCalculator
{
    decimal Calculate(IEnumerable<TradeRowData> group);
}