namespace Aggraze.Domain;

public record InsightResult(
    string InsightName,
    Dictionary<int, Dictionary<string, TimeSpan>> YearMonthData,
    Dictionary<string, TimeSpan> Summary);
    