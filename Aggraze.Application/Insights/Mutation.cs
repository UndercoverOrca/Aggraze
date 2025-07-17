using Aggraze.Domain.Calculators;
using Aggraze.Domain.Types;
using LanguageExt;

using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the gain/loss of the of a traders' taken (backtest) trades, expressed in %.
/// </summary>
public class Mutation : InsightBase, IInsight
{
    public string Name => "Mutation";

    private readonly IMutationCalculator _mutationCalculator;

    public Mutation(IMutationCalculator mutationCalculator) =>
        _mutationCalculator = mutationCalculator;

    public Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades) =>
        trades
            .Any(ContainsRequiredHeaders)
        ? Some(Calculate(trades
            .Where(ContainsRequiredHeaders)
            .ToList()))
        : None;
    
    private static Func<TradeRow, bool> ContainsRequiredHeaders => x =>
        x.Data.Date.IsSome
        && x.Data.Mutation.IsSome
        && x.Data.Result.IsSome;
    
    private IInsightResult Calculate(IReadOnlyList<TradeRow> trades)
    {
        var groupedByYearAndMonth = GroupTradesByYearAndMonth(trades);

        var yearMonthData = new Dictionary<int, Dictionary<string, decimal>>();
        var summaryHelper = new Dictionary<string, List<decimal>>();

        foreach (var group in groupedByYearAndMonth)
        {
            var maximumDrawdown = this._mutationCalculator.Calculate(group);
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