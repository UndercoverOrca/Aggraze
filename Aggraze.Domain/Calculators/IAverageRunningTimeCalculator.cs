using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IAverageRunningTimeCalculator
{
    IInsightResult Calculate(string name, IReadOnlyList<TradeRow> trades);
}