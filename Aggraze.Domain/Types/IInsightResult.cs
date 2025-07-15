namespace Aggraze.Domain.Types;

public interface IInsightResult
{
    string InsightName { get; }
    IReadOnlyDictionary<int, IReadOnlyDictionary<string, object>> YearMonthData { get; }
    ISummary Summary { get; }
}