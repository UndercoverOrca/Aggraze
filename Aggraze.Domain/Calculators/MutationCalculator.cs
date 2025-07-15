using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MutationCalculator : Calculator, IMutationCalculator
{
    private const decimal PercentageDivisor = 100m;
    
    public IInsightResult Calculate(string name, IReadOnlyList<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var averageMutation = group.Value
                .GroupBy(trade => trade.Result)
                .ToDictionary(
                    g => g.Key,
                    g => g.Count());
            
            var mutation = (decimal)(averageMutation.GetValueOrDefault(Result.Win, 0)
                                    - averageMutation.GetValueOrDefault(Result.Loss, 0));
            
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, mutation);
        }
        
        var summary = new Summary<decimal>(
            SummaryType.Average,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => x.Value.Average()));
        
        var orderedYearMonthData = OrderYearMonthDataByYear(yearMonthData);
        return new InsightResult<decimal>(name, orderedYearMonthData, summary);
    }
}