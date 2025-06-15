using Aggraze.Application.Insights;
using Aggraze.Domain;

namespace Aggraze.Application;

public class AggregationOrchestratorService
{
    private readonly IEnumerable<IInsight> _insights;

    public AggregationOrchestratorService(IEnumerable<IInsight> insights)
    {
        _insights = insights;
    }

    public IReadOnlyList<InsightResult> RunAllInsights(List<TradeData> trades) =>
        _insights
            .Select(insight => insight.GenerateInsight(trades))
            .ToList();
}