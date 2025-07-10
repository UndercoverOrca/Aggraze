using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMaximumDrawdownCalculator
{
    InsightResult Calculate(string name, IEnumerable<TradeRow> trades);
}