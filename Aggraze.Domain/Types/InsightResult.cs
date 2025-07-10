namespace Aggraze.Domain.Types;

public record InsightResult(
    string InsightName,
    Dictionary<int, Dictionary<string, TimeSpan>> YearMonthData,
    Summary Summary);
    