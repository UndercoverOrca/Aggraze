using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMutationCalculator
{
    decimal Calculate(IEnumerable<TradeRowData> group);
}