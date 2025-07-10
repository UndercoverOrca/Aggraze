using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Domain.Calculators;

public class MaximumDrawdownCalculator : Calculator, IMaximumDrawdownCalculator
{
    public InsightResult Calculate(string name, IEnumerable<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var maximumDrawdown = group.Value
                .Select(trade => trade.MaximumDrawdown)
                .Max(x => x.ValueUnsafe());

            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, maximumDrawdown);
        }

        var summary = new Summary(
            SummaryType.Maximum,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => x.Value.Max()
            ));

        var orderedYearMonthData = yearMonthData
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        return new InsightResult(name, orderedYearMonthData, summary);
    }
}