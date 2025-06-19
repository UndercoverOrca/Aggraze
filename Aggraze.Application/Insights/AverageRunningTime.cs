using Aggraze.Domain;
using Aggraze.Domain.Calculators;

namespace Aggraze.Application.Insights;

/// <summary>
/// Shows the average running time of a traders' taken (backtest) trades.
/// </summary>
public class AverageRunningTime : IInsight
{
    private readonly IAverageRunningTimeCalculator _averageRunningTimeCalculator;

    public AverageRunningTime(IAverageRunningTimeCalculator averageRunningTimeCalculator)
    {
        _averageRunningTimeCalculator = averageRunningTimeCalculator;
    }

    public string Name => "Average running time";
    
    public InsightResult GenerateInsight(IEnumerable<TradeRow> trades) =>
        this._averageRunningTimeCalculator.CalculateAverageRunningTime(Name, trades);
    
}