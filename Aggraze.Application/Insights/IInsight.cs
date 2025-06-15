using Aggraze.Domain;

namespace Aggraze.Application.Insights;

public interface IInsight
{
    string Name { get; }

    InsightResult GenerateInsight(IEnumerable<TradeData> trades);
}