using Aggraze.Domain;
using Aggraze.Domain.Calculators;
using LanguageExt;
using static LanguageExt.Prelude;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades.
/// </summary>
public class AverageRunningTime : IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeCalculator;
    private static string[] RequiredHeaders = ["Closing time", "Open time", "Date"];

    public AverageRunningTime(IAverageRunningTimeCalculator averageRunningTimeCalculator)
    {
        _averageRunningTimeCalculator = averageRunningTimeCalculator;
    }

    public string Name => "Average running time";
    
    public Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades) =>
         !RequiredHeaders
            .All(header => trades.First().Value.ContainsKey(header))
            ? None
            : Some(this._averageRunningTimeCalculator.CalculateAverageRunningTime(Name, trades));
}