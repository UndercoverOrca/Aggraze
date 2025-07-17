using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumRiskRewardWinningTradesCalculator : Calculator, IMaximumRiskRewardWinningTradesCalculator
{
    public decimal Calculate(KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>> group) =>
        group.Value
            .Where(x => x.Result == Result.Win)
            .Min(x => x.MaximumResult)
            .Value();
}