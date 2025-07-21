using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class AverageRunningTimeCalculator : Calculator, IAverageRunningTimeCalculator
{
    public TimeSpan Calculate(IEnumerable<TradeRowData> group) =>
        TimeSpan.FromTicks(
            (long)group
                .Select(trade => trade.ClosingTime.Value() - trade.OpenTime.Value())
                .Average(x => x.Ticks));
}