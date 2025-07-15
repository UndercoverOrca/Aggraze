namespace Aggraze.Domain.Types;

public record InsightResult<T>(
    string InsightName,
    Dictionary<int, Dictionary<string, T>> YearMonthData,
    ISummary Summary) : IInsightResult
{
    IReadOnlyDictionary<int, IReadOnlyDictionary<string, object>> IInsightResult.YearMonthData =>
        YearMonthData.ToDictionary(
            outer => outer.Key, IReadOnlyDictionary<string, object> (outer) => outer.Value
                .ToDictionary(inner => inner.Key, inner => (object)inner.Value!));
}