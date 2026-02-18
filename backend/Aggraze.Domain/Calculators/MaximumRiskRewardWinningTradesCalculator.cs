using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumRiskRewardWinningTradesCalculator : Calculator, IMaximumRiskRewardWinningTradesCalculator
{
    public decimal Calculate(IEnumerable<TradeRowData> group) =>
        group
            .Where(x => x.Result == Result.Win)
            .Min(x => x.MaximumResult)
            .Value();
}