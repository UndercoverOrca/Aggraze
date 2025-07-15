using Aggraze.Domain.Types;
using LanguageExt;

namespace Aggraze.Application.Insights;

public interface IInsight
{
    string Name { get; }

    Option<IInsightResult> GenerateInsight(IReadOnlyList<TradeRow> trades);
}