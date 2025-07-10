using Aggraze.Application.Insights;
using Aggraze.Domain.Types;
using LanguageExt.UnsafeValueAccess;

namespace Aggraze.Application.Services;

public record AggregationOrchestratorService(IEnumerable<IInsight> Insights)
{
    public IReadOnlyList<InsightResult> RunAllInsights(IReadOnlyList<TradeRow> trades) =>
        this.Insights
            .Select(insight => insight.GenerateInsight(trades))
            .Where(x => x.IsSome)
            .Select(x => x.ValueUnsafe())
            .ToList();
}