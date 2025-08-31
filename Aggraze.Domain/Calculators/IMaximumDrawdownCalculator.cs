using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumDrawdownCalculator
{
    Pips Calculate(IEnumerable<TradeRowData> group);
}