namespace Aggraze.Domain;

public record InsightDataPoint(
    int Year,
    int Month,
    AggregationType AggregationType,
    string Value);