using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumRiskRewardWinningTradesCalculator
{
    decimal Calculate(KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>> group);
}