using Aggraze.Domain.Types;

namespace Aggraze.Domain.Calculators;

public class MutationCalculator : Calculator, IMutationCalculator
{
    private const decimal PercentageDivisor = 100m;

    public decimal Calculate(KeyValuePair<(int Year, int Month), IEnumerable<TradeRowData>> group)
    {
        var averageMutation = group.Value
            .GroupBy(trade => trade.Result)
            .ToDictionary(
                g => g.Key,
                g => g.Count());

        return averageMutation.GetValueOrDefault(Result.Win, 0)
                - averageMutation.GetValueOrDefault(Result.Loss, 0);
    }
}