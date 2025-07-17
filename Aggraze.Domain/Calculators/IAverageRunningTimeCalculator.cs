using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IAverageRunningTimeCalculator
{
    TimeSpan Calculate(KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>> group);
}