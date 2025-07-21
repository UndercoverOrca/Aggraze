using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IAverageRunningTimeCalculator
{
    TimeSpan Calculate(IEnumerable<TradeRowData> group);
}