using Aggraze.Domain;
using LanguageExt;

namespace Aggraze.Application.Insights;

public interface IInsight
{
    string Name { get; }

    Option<InsightResult> GenerateInsight(IEnumerable<TradeRow> trades);
}