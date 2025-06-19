namespace Aggraze.Domain.Calculators;

public interface IAverageRunningTimeCalculator
{
    InsightResult CalculateAverageRunningTime(string name, IEnumerable<TradeRow> trades);
}