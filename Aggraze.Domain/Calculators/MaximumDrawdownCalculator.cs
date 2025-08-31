using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumDrawdownCalculator : Calculator, IMaximumDrawdownCalculator
{
    public Pips Calculate(IEnumerable<TradeRowData> group) => 
        group
            .Select(trade => trade.MaximumDrawdown)
            .Max(x => x.Value());
}