using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public interface IMutationCalculator
{
    IInsightResult Calculate(string name, IReadOnlyList<TradeRow> trades);
}