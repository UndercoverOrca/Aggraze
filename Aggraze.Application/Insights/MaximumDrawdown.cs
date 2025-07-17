using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the maximum drawdown of the traders' taken (backtest) trades that resulted in a win.
/// </summary>
public class MaximumDrawdown : InsightBase, IInsight
{
    private readonly IMaximumDrawdownCalculator _maximumDrawdownCalculator;

    public MaximumDrawdown(IMaximumDrawdownCalculator maximumDrawdownCalculator) =>
        this._maximumDrawdownCalculator = maximumDrawdownCalculator;

    public string Name => "Maximum drawdown";

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .Any(ContainsRequiredValues)
            ? Some(Calculate(trades
                .Where(ContainsRequiredValues)
                .ToList()))
            : None;

    private static Func<TradeRow, bool> ContainsRequiredValues => x =>
        x.Data.Date.IsSome
        && x.Data.MaximumDrawdown.IsSome;

    private IInsightResult Calculate(IReadOnlyList<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var maximumDrawdown = this._maximumDrawdownCalculator.Calculate(group);
            AddYearMonthSummary(yearMonthData, summaryHelper, group.Key, maximumDrawdown);
        }

        var summary = new Summary<decimal>(
            SummaryType.Maximum,
            summaryHelper.ToDictionary(
                x => x.Key,
                x => x.Value.Max()));

        var orderedYearMonthData = yearMonthData
            .OrderBy(x => x.Key)
            .ToDictionary(x => x.Key, x => x.Value);

        return new InsightResult<decimal>(Name, orderedYearMonthData, summary);
    }
}