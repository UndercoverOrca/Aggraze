using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class AverageRunningTimeCalculator : Calculator, IAverageRunningTimeCalculator
{
    public TimeSpan Calculate(KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>> group) =>
        TimeSpan.FromTicks(
            (long)group.Value
                .Select(trade => trade.ClosingTime.Value() - trade.OpenTime.Value())
                .Average(x => x.Ticks));
}