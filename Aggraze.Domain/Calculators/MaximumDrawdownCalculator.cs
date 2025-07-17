using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumDrawdownCalculator : Calculator, IMaximumDrawdownCalculator
{
    public decimal Calculate(KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>> group) => 
        group.Value
            .Select(trade => trade.MaximumDrawdown)
            .Max(x => x.Value());
}