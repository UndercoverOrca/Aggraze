using Aggraze.Domain;
using Aggraze.Domain.Calculators;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades that resulted in a loss.
/// </summary>
public class AverageRunningTimeLosers : IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeLosersCalculator;
    private static string[] requiredHeaders = ["Closing time", "Open time", "Result", "Date"];

    public AverageRunningTimeLosers(IAverageRunningTimeCalculator averageRunningTimeLosersCalculator) =>
        this._averageRunningTimeLosersCalculator = averageRunningTimeLosersCalculator;

    public string Name => "Average running time losers";

    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades)
    {
        var filteredTrades = trades.Where(x => x.Value["Result"] == "Loss").ToList();
        return filteredTrades.Any() &&
               requiredHeaders
                   .All(header => filteredTrades.First().Value.ContainsKey(header))
            ? Some(this._averageRunningTimeLosersCalculator.CalculateAverageRunningTime(Name, filteredTrades))
            : None;
    }
}