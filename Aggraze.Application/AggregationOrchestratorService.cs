using Aggraze.Application.Insights;
using Aggraze.Domain;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Application;

public class AggregationOrchestratorService
{
    private readonly IEnumerable<IInsight> _insights;

    public AggregationOrchestratorService(IEnumerable<IInsight> insights)
    {
        _insights = insights;
    }

    public IReadOnlyList<InsightResult> RunAllInsights(IReadOnlyList<TradeRow> trades) =>
        _insights
            .Select(insight => insight.GenerateInsight(trades))
            .Where(x => x.IsSome)
            .Select(x => x.ValueUnsafe())
            .ToList();
}