using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumRiskRewardWinningTradesCalculator : Calculator, IMaximumRiskRewardWinningTradesCalculator
{
    public IInsightResult Calculate(string name, IReadOnlyList<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var averageMutation = group.Value
                .Where(x => x.Result == Result.Win)
                .Min(x => x.MaximumResult)
                .Value();
            
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, averageMutation);
        }

        var summary = new Summary<decimal>(
            SummaryType.Maximum,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => x.Value.Max()));
        
        var orderedYearMonthData = yearMonthData
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        return new InsightResult<decimal>(name, orderedYearMonthData, summary);
    }
}