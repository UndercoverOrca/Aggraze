using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IAverageRunningTimeCalculator
{
    InsightResult Calculate(string name, IEnumerable<TradeRow> trades);
}