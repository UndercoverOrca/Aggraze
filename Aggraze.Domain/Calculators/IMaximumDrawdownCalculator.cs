using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumDrawdownCalculator
{
    decimal Calculate(IEnumerable<TradeRowData> group);
}