using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MutationCalculator : Calculator, IMutationCalculator
{
    private const decimal PercentageDivisor = 100m;

    public decimal Calculate(IEnumerable<TradeRowData> group) =>
        group.Sum(x => x.Mutation.Value());
}